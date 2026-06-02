using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Warranty.Service.Common;
using Warranty.Service.Common.Configuration;
using Warranty.Service.Common.Messaging;
using Warranty.Service.Common.Messaging.Const;
using Warranty.Service.Common.Pagination;
using Warranty.Service.Data;
using Warranty.Service.Entities;
using Warranty.Service.Mappers;
using Warranty.Service.Models;
using Warranty.Service.Services.Contracts;

namespace Warranty.Service.Services;

public class WarrantyService : IWarrantyService
{
    private readonly WarrantyDbContext _dbContext;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<WarrantyService> _logger;
    private readonly string _eventsTopic;

    public WarrantyService(
        WarrantyDbContext dbContext,
        IEventPublisher eventPublisher,
        IOptions<KafkaOptions> kafkaOptions,
        ILogger<WarrantyService> logger)
    {
        _dbContext = dbContext;
        _eventPublisher = eventPublisher;
        _logger = logger;
        _eventsTopic = kafkaOptions.Value.Producers.WarrantyEvents.Topic ?? WarrantyTopics.Events;
    }

    public async Task<Result<CursorPage<WarrantyResponse>>> GetByCustomerAsync(GetWarrantiesByCustomerRequest request, CancellationToken ct)
    {
        var cursor = CursorEncoder.Decode<WarrantyCursor>(request.Cursor!);

        if (cursor is null && !string.IsNullOrEmpty(request.Cursor))
        {
            _logger.LogWarning("Malformed cursor received from client: {Cursor}", request.Cursor);
        }

        var query = _dbContext.Warranties
            .Where(w => w.CustomerId == request.CustomerId);

        if (cursor is not null)
        {
            query = query.Where(w => EF.Functions.LessThanOrEqual(
                ValueTuple.Create(w.CreatedAt, w.Id),
                ValueTuple.Create(cursor.CreatedAt, cursor.Id)));
        }

        return await query
            .OrderByDescending(w => w.CreatedAt)
            .ThenByDescending(w => w.Id)
            .Select(w => w.ToResponse())
            .ToCursorPageAsync(
                request,
                item => new WarrantyCursor(item.CreatedAt, item.Id),
                ct);
    }

    public async Task<Result<WarrantyResponse>> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var warranty = await _dbContext.Warranties
            .Where(w => w.Id == id)
            .Select(w => w.ToResponse())
            .FirstOrDefaultAsync(ct);

        if (warranty is null)
        {
            return Error.NotFound($"Warranty {id} not found");
        }

        return warranty;
    }

    public async Task<Result<WarrantyResponse>> CreateAsync(CreateWarrantyRequest request, CancellationToken ct)
    {
        var warranty = request.ToEntity();
        _dbContext.Warranties.Add(warranty);

        await _eventPublisher.PublishAsync(
            warranty.ToRegisteredEvent(),
            new EventMetadata(
                Type: WarrantyEventTypes.Registered,
                Topic: _eventsTopic,
                AggregateType: nameof(WarrantyEntity),
                AggregateId: warranty.Id,
                PartitionKey: warranty.CustomerId.ToString()),
            ct);

        await _dbContext.SaveChangesAsync(ct);

        _logger.LogInformation(
            "Warranty {WarrantyId} created for customer {CustomerId}",
            warranty.Id,
            warranty.CustomerId);

        return warranty.ToResponse();
    }
}

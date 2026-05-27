using Microsoft.EntityFrameworkCore;
using Warranty.Service.Common;
using Warranty.Service.Common.Pagination;
using Warranty.Service.Data;
using Warranty.Service.Mappers;
using Warranty.Service.Models;
using Warranty.Service.Services.Contracts;

namespace Warranty.Service.Services;

public class WarrantyService : IWarrantyService
{
    private readonly WarrantyDbContext _dbContext;
    private readonly ILogger<WarrantyService> _logger;

    public WarrantyService(WarrantyDbContext dbContext, ILogger<WarrantyService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
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
}

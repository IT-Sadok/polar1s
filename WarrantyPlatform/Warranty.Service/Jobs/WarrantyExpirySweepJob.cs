using Microsoft.EntityFrameworkCore;
using Warranty.Service.Common.Messaging;
using Warranty.Service.Common.Messaging.Const;
using Warranty.Service.Data;
using Warranty.Service.Entities;
using Warranty.Service.Entities.Const;
using Warranty.Service.Mappers;

namespace Warranty.Service.Jobs;

public class WarrantyExpirySweepJob : BackgroundService
{
    private static readonly TimeSpan SweepInterval = TimeSpan.FromSeconds(30);
    private const int BatchSize = 100;

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<WarrantyExpirySweepJob> _logger;

    public WarrantyExpirySweepJob(IServiceScopeFactory scopeFactory, ILogger<WarrantyExpirySweepJob> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await SweepAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "WarrantyExpirySweepJob iteration failed");
            }

            try
            {
                await Task.Delay(SweepInterval, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }

    private async Task SweepAsync(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WarrantyDbContext>();
        var publisher = scope.ServiceProvider.GetRequiredService<IEventPublisher>();
        var now = DateTime.UtcNow;

        var expired = await dbContext.Warranties
            .Where(w => w.Status == WarrantyStatus.Active && w.ExpiresAt < now)
            .Take(BatchSize)
            .ToListAsync(ct);

        if (expired.Count == 0)
        {
            return;
        }

        foreach(var warranty in expired)
        {
            warranty.Status = WarrantyStatus.Expired;
            await publisher.PublishAsync(warranty.ToExpiredEvent(), new EventMetadata(
                    Type: WarrantyEventTypes.Expired,
                    Topic: WarrantyTopics.Events,
                    AggregateType: nameof(WarrantyEntity),
                    AggregateId: warranty.Id,
                    PartitionKey: warranty.CustomerId.ToString()), ct);
        }

        await dbContext.SaveChangesAsync(ct);
        _logger.LogInformation("Expired {Count} warranties", expired.Count);
    }
}

using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Warranty.Service.Common.Messaging.Const;
using Warranty.Service.Contracts.Events;
using Warranty.Service.Data;
using Warranty.Service.Entities;

namespace Warranty.Service.Common.Messaging;

public class OutboxRelayJob : BackgroundService
{
    private static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(15);
    private static readonly TimeSpan FlushTimeout = TimeSpan.FromSeconds(10);
    private const int BatchSize = 100;

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IProducer<string, byte[]> _producer;
    private readonly IAsyncSerializer<WarrantyRegisteredEvent> _warrantyRegisteredSerializer;
    private readonly ILogger<OutboxRelayJob> _logger;

    public OutboxRelayJob(
        IServiceScopeFactory scopeFactory,
        IProducer<string, byte[]> producer,
        IAsyncSerializer<WarrantyRegisteredEvent> warrantyRegisteredSerializer,
        ILogger<OutboxRelayJob> logger)
    {
        _scopeFactory = scopeFactory;
        _producer = producer;
        _warrantyRegisteredSerializer = warrantyRegisteredSerializer;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await PollAndPublishAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OutboxRelayJob iteration failed");
            }

            try
            {
                await Task.Delay(PollInterval, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }

    private async Task PollAndPublishAsync(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WarrantyDbContext>();

        var pending = await dbContext.OutboxMessages
            .Where(o => o.ProcessedAt == null)
            .OrderBy(o => o.CreatedAt)
            .Take(BatchSize)
            .ToListAsync(ct);

        if (pending.Count == 0)
        {
            return;
        }

        var deliveryResults = new ConcurrentDictionary<Guid, bool>();

        foreach (var outbox in pending)
        {
            try
            {
                var valueBytes = await SerializeAsync(outbox);
                var message = BuildKafkaMessage(outbox, valueBytes);
                var captured = outbox;

                _producer.Produce(outbox.Topic, message, report =>
                {
                    var success = !report.Error.IsError;
                    deliveryResults[captured.Id] = success;

                    if (!success)
                    {
                        _logger.LogError(
                            "Failed to publish outbox {OutboxId} ({MessageId}): {Reason}",
                            captured.Id, captured.MessageId, report.Error.Reason);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to enqueue outbox {OutboxId} for publish", outbox.Id);
                deliveryResults[outbox.Id] = false;
            }
        }

        _producer.Flush(FlushTimeout);

        var successfulIds = deliveryResults
            .Where(kvp => kvp.Value)
            .Select(kvp => kvp.Key)
            .ToList();

        if (successfulIds.Count == 0)
        {
            return;
        }

        var now = DateTime.UtcNow;
        await dbContext.OutboxMessages
            .Where(o => successfulIds.Contains(o.Id))
            .ExecuteUpdateAsync(
                s => s.SetProperty(o => o.ProcessedAt, now),
                ct);

        _logger.LogInformation(
            "Published {SuccessCount}/{TotalCount} outbox messages",
            successfulIds.Count, pending.Count);
    }

    private Task<byte[]> SerializeAsync(OutboxMessageEntity outbox)
    {
        var ctx = new SerializationContext(MessageComponentType.Value, outbox.Topic);

        return outbox.Type switch
        {
            WarrantyEventTypes.Registered =>
                _warrantyRegisteredSerializer.SerializeAsync(
                    JsonSerializer.Deserialize<WarrantyRegisteredEvent>(outbox.Payload, JsonOptions)!,
                    ctx),
            _ => throw new InvalidOperationException($"Unknown event type: {outbox.Type}")
        };
    }

    private static Message<string, byte[]> BuildKafkaMessage(OutboxMessageEntity outbox, byte[] valueBytes)
    {
        return new Message<string, byte[]>
        {
            Key = outbox.PartitionKey ?? string.Empty,
            Value = valueBytes,
            Headers = new Headers
            {
                { CloudEventHeaders.Id, Encoding.UTF8.GetBytes(outbox.MessageId.ToString()) },
                { CloudEventHeaders.Type, Encoding.UTF8.GetBytes(outbox.Type) },
                { CloudEventHeaders.Source, Encoding.UTF8.GetBytes(EventSources.WarrantyService) },
                { CloudEventHeaders.Time, Encoding.UTF8.GetBytes(outbox.OccurredAt.ToString("O", CultureInfo.InvariantCulture)) },
                { CloudEventHeaders.SpecVersion, Encoding.UTF8.GetBytes(CloudEventHeaders.CurrentSpecVersion) },
            }
        };
    }
}

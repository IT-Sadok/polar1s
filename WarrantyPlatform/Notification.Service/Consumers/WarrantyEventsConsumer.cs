using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema.NewtonsoftJson.Generation;
using Notification.Service.Common.Configuration;
using Notification.Service.Common.Messaging.Const;
using Notification.Service.Contracts.Events;
using Notification.Service.Data;
using Notification.Service.Entities;

namespace Notification.Service.Consumers;

public class WarrantyEventsConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ISchemaRegistryClient _schemaRegistry;
    private readonly KafkaOptions _kafkaOptions;
    private readonly ILogger<WarrantyEventsConsumer> _logger;

    public WarrantyEventsConsumer(
        IServiceScopeFactory scopeFactory,
        ISchemaRegistryClient schemaRegistry,
        IOptions<KafkaOptions> kafkaOptions,
        ILogger<WarrantyEventsConsumer> logger)
    {
        _scopeFactory = scopeFactory;
        _schemaRegistry = schemaRegistry;
        _kafkaOptions = kafkaOptions.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        var consumerOptions = _kafkaOptions.Consumers.WarrantyEvents;
        var topic = consumerOptions.Topic ?? WarrantyTopics.Events;

        var config = new ConsumerConfig
        {
            BootstrapServers = _kafkaOptions.BootstrapServers,
            GroupId = consumerOptions.GroupId,
            AutoOffsetReset = consumerOptions.AutoOffsetReset,
            EnableAutoCommit = false,
        };

        var schemaGeneratorSettings = new NewtonsoftJsonSchemaGeneratorSettings
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        };
        var deserializer = new JsonDeserializer<WarrantyRegisteredEvent>(
            _schemaRegistry,
            config: null,
            jsonSchemaGeneratorSettings: schemaGeneratorSettings);

        using var consumer = new ConsumerBuilder<string, WarrantyRegisteredEvent>(config)
            .SetValueDeserializer(deserializer.AsSyncOverAsync())
            .SetErrorHandler((_, e) => _logger.LogError("Kafka consumer error: {Reason}", e.Reason))
            .Build();

        consumer.Subscribe(topic);
        _logger.LogInformation(
            "Subscribed to {Topic} as group {GroupId} (offset reset: {OffsetReset})",
            topic, consumerOptions.GroupId, consumerOptions.AutoOffsetReset);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ConsumeResult<string, WarrantyRegisteredEvent> result;
                try
                {
                    result = consumer.Consume(stoppingToken);
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError(ex, "Failed to consume message");
                    continue;
                }

                if (result?.Message is null)
                {
                    continue;
                }

                await HandleAsync(result, stoppingToken);
                consumer.Commit(result);
            }
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            consumer.Close();
        }
    }

    private async Task HandleAsync(ConsumeResult<string, WarrantyRegisteredEvent> result, CancellationToken ct)
    {
        var messageId = ExtractMessageId(result.Message.Headers);
        var eventType = ExtractHeader(result.Message.Headers, CloudEventHeaders.Type);

        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();

        var alreadyProcessed = await dbContext.InboxMessages
            .AnyAsync(i => i.MessageId == messageId, ct);

        if (alreadyProcessed)
        {
            _logger.LogInformation("Duplicate message {MessageId} skipped", messageId);
            return;
        }

        dbContext.InboxMessages.Add(new InboxMessageEntity
        {
            MessageId = messageId,
            Type = eventType,
            ProcessedAt = DateTime.UtcNow,
        });
        await dbContext.SaveChangesAsync(ct);

        var payload = result.Message.Value;
        _logger.LogInformation(
            "Sent notification for WarrantyRegistered {MessageId}: warranty {WarrantyId} registered for customer {CustomerId}",
            messageId, payload.WarrantyId, payload.CustomerId);
    }

    private static Guid ExtractMessageId(Headers headers)
    {
        var raw = ExtractHeader(headers, CloudEventHeaders.Id);
        return Guid.TryParse(raw, out var id) ? id : Guid.Empty;
    }

    private static string ExtractHeader(Headers headers, string key)
    {
        return headers.TryGetLastBytes(key, out var bytes)
            ? Encoding.UTF8.GetString(bytes)
            : string.Empty;
    }
}

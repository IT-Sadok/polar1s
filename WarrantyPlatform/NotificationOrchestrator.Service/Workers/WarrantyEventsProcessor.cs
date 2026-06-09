using System.Globalization;
using System.Text;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using NotificationOrchestrator.Service.Common.Configuration;
using NotificationOrchestrator.Service.Common.Messaging.Const;
using NotificationOrchestrator.Service.Contracts.Commands;
using NotificationOrchestrator.Service.Translators;

namespace NotificationOrchestrator.Service.Workers;

public class WarrantyEventsProcessor : BackgroundService
{
    private readonly IProducer<string, byte[]> _producer;
    private readonly IAsyncSerializer<SendNotificationCommand> _commandSerializer;
    private readonly IReadOnlyDictionary<string, IWarrantyEventTranslator> _translatorsByType;
    private readonly KafkaOptions _kafkaOptions;
    private readonly ILogger<WarrantyEventsProcessor> _logger;

    public WarrantyEventsProcessor(
        IProducer<string, byte[]> producer,
        IAsyncSerializer<SendNotificationCommand> commandSerializer,
        IEnumerable<IWarrantyEventTranslator> translators,
        IOptions<KafkaOptions> kafkaOptions,
        ILogger<WarrantyEventsProcessor> logger)
    {
        _producer = producer;
        _commandSerializer = commandSerializer;
        _translatorsByType = translators.ToDictionary(t => t.EventType);
        _kafkaOptions = kafkaOptions.Value;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();

        var consumerOptions = _kafkaOptions.Consumers.WarrantyEvents;
        var sourceTopic = consumerOptions.Topic ?? Topics.WarrantyEvents;

        var config = new ConsumerConfig
        {
            BootstrapServers = _kafkaOptions.BootstrapServers,
            GroupId = consumerOptions.GroupId,
            AutoOffsetReset = consumerOptions.AutoOffsetReset,
            EnableAutoCommit = false,
        };

        using var consumer = new ConsumerBuilder<string, byte[]>(config)
            .SetErrorHandler((_, e) => _logger.LogError("Kafka consumer error: {Reason}", e.Reason))
            .Build();

        consumer.Subscribe(sourceTopic);
        _logger.LogInformation(
            "Subscribed to {Topic} as group {GroupId} (offset reset: {OffsetReset})",
            sourceTopic, consumerOptions.GroupId, consumerOptions.AutoOffsetReset);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                ConsumeResult<string, byte[]> result;
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

                await HandleAsync(result, sourceTopic, stoppingToken);
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

    private async Task HandleAsync(ConsumeResult<string, byte[]> result, string sourceTopic, CancellationToken ct)
    {
        var eventType = ExtractHeader(result.Message.Headers, CloudEventHeaders.Type);
        var sourceMessageId = ExtractHeader(result.Message.Headers, CloudEventHeaders.Id);

        if (!_translatorsByType.TryGetValue(eventType, out var translator))
        {
            _logger.LogWarning("Unhandled event type '{EventType}', skipping", eventType);
            return;
        }

        var command = await translator.TranslateAsync(result.Message.Value, sourceTopic, sourceMessageId);
        await PublishAsync(command, ct);
    }

    private async Task PublishAsync(SendNotificationCommand command, CancellationToken ct)
    {
        var targetTopic = _kafkaOptions.Producers.NotificationCommands.Topic ?? Topics.NotificationCommands;
        var ctx = new SerializationContext(MessageComponentType.Value, targetTopic);
        var valueBytes = await _commandSerializer.SerializeAsync(command, ctx);

        var message = new Message<string, byte[]>
        {
            Key = command.Recipient,
            Value = valueBytes,
            Headers = new Headers
            {
                { CloudEventHeaders.Id, Encoding.UTF8.GetBytes(command.NotificationId.ToString()) },
                { CloudEventHeaders.Type, Encoding.UTF8.GetBytes(CommandTypes.SendNotification) },
                { CloudEventHeaders.Source, Encoding.UTF8.GetBytes(EventSources.NotificationOrchestrator) },
                { CloudEventHeaders.Time, Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString("O", CultureInfo.InvariantCulture)) },
                { CloudEventHeaders.SpecVersion, Encoding.UTF8.GetBytes(CloudEventHeaders.CurrentSpecVersion) },
            }
        };

        await _producer.ProduceAsync(targetTopic, message, ct);

        _logger.LogInformation(
            "Translated {NotificationId} -> SendNotificationCommand for recipient {Recipient}",
            command.NotificationId, command.Recipient);
    }

    private static string ExtractHeader(Headers headers, string key)
    {
        return headers.TryGetLastBytes(key, out var bytes)
            ? Encoding.UTF8.GetString(bytes)
            : string.Empty;
    }
}

using Confluent.Kafka;

namespace Notification.Service.Common.Configuration;

public sealed class KafkaOptions
{
    public const string SectionName = "Kafka";

    public string BootstrapServers { get; set; } = string.Empty;
    public string SchemaRegistryUrl { get; set; } = string.Empty;
    public KafkaConsumersOptions Consumers { get; set; } = new();
}

public sealed class KafkaConsumersOptions
{
    public ConsumerOptions NotificationCommands { get; set; } = new();
}

public sealed class ConsumerOptions
{
    public string? Topic { get; set; }
    public string GroupId { get; set; } = string.Empty;
    public AutoOffsetReset AutoOffsetReset { get; set; } = AutoOffsetReset.Latest;
}

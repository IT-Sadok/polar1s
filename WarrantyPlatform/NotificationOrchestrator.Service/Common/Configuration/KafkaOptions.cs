using Confluent.Kafka;

namespace NotificationOrchestrator.Service.Common.Configuration;

public sealed class KafkaOptions
{
    public const string SectionName = "Kafka";

    public string BootstrapServers { get; set; } = string.Empty;
    public string SchemaRegistryUrl { get; set; } = string.Empty;
    public KafkaConsumersOptions Consumers { get; set; } = new();
    public KafkaProducersOptions Producers { get; set; } = new();
}

public sealed class KafkaConsumersOptions
{
    public ConsumerOptions WarrantyEvents { get; set; } = new();
}

public sealed class ConsumerOptions
{
    public string? Topic { get; set; }
    public string GroupId { get; set; } = string.Empty;
    public AutoOffsetReset AutoOffsetReset { get; set; } = AutoOffsetReset.Latest;
}

public sealed class KafkaProducersOptions
{
    public ProducerOptions NotificationCommands { get; set; } = new();
}

public sealed class ProducerOptions
{
    public string? Topic { get; set; }
    public Acks Acks { get; set; } = Acks.All;
}

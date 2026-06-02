using Confluent.Kafka;

namespace Warranty.Service.Common.Configuration;

public sealed class KafkaOptions
{
    public const string SectionName = "Kafka";

    public string BootstrapServers { get; set; } = string.Empty;
    public string SchemaRegistryUrl { get; set; } = string.Empty;
    public KafkaProducersOptions Producers { get; set; } = new();
}

public sealed class KafkaProducersOptions
{
    public ProducerOptions WarrantyEvents { get; set; } = new();
}

public sealed class ProducerOptions
{
    public string? Topic { get; set; }
    public Acks Acks { get; set; } = Acks.All;
}

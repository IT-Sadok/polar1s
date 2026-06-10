using Warranty.Service.Entities.Contracts;

namespace Warranty.Service.Entities;

public class OutboxMessageEntity : IAuditable
{
    public Guid Id { get; set; }
    public Guid MessageId { get; set; }
    public string AggregateType { get; set; } = string.Empty;
    public Guid AggregateId { get; set; }
    public string Topic { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? PartitionKey { get; set; }
    public string Payload { get; set; } = string.Empty;
    public DateTime OccurredAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
}

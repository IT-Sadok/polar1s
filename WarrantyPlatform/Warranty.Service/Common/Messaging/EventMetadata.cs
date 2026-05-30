namespace Warranty.Service.Common.Messaging;

public sealed record EventMetadata(
    string Type,
    string Topic,
    string AggregateType,
    Guid AggregateId,
    string? PartitionKey = default,
    DateTime? OccurredAt = default);

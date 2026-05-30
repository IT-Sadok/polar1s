using System.Text.Json;
using Warranty.Service.Data;
using Warranty.Service.Entities;

namespace Warranty.Service.Common.Messaging;

public class OutboxEventPublisher(WarrantyDbContext dbContext) : IEventPublisher
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public Task PublishAsync<TEvent>(TEvent payload, EventMetadata metadata, CancellationToken ct)
        where TEvent : class
    {
        var serializedPayload = JsonSerializer.Serialize(payload, JsonOptions);
        var entity = new OutboxMessageEntity
        { 
            Id = Guid.NewGuid(),
            MessageId = Guid.NewGuid(),
            AggregateType = metadata.AggregateType,
            AggregateId = metadata.AggregateId,
            Topic = metadata.Topic,
            Type = metadata.Type,
            PartitionKey = metadata.PartitionKey,
            Payload = serializedPayload,
            OccurredAt = metadata.OccurredAt ?? DateTime.UtcNow,
        };

        dbContext.OutboxMessages.Add(entity);
        return Task.CompletedTask;
    }
}

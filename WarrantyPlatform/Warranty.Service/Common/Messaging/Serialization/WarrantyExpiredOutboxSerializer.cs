using Confluent.SchemaRegistry;
using Warranty.Service.Common.Messaging.Const;
using Warranty.Service.Contracts.Events;

namespace Warranty.Service.Common.Messaging.Serialization;

public sealed class WarrantyExpiredOutboxSerializer(ISchemaRegistryClient r)
    : OutboxEventSerializerBase<WarrantyExpiredEvent>(r)
{
    public override string EventType => WarrantyEventTypes.Expired;
}

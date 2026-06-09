using Confluent.SchemaRegistry;
using Warranty.Service.Common.Messaging.Const;
using Warranty.Service.Contracts.Events;

namespace Warranty.Service.Common.Messaging.Serialization;

public sealed class WarrantyRegisteredOutboxSerializer(ISchemaRegistryClient r)
    : OutboxEventSerializerBase<WarrantyRegisteredEvent>(r)
{
    public override string EventType => WarrantyEventTypes.Registered;
}

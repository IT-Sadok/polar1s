using Confluent.SchemaRegistry;
using NotificationOrchestrator.Service.Common.Messaging.Const;
using NotificationOrchestrator.Service.Contracts.Commands;
using NotificationOrchestrator.Service.Contracts.Events;

namespace NotificationOrchestrator.Service.Translators;

public sealed class WarrantyRegisteredTranslator(ISchemaRegistryClient schemaRegistry)
    : WarrantyEventTranslatorBase<WarrantyRegisteredEvent>(schemaRegistry)
{
    public override string EventType => EventTypes.WarrantyRegistered;

    protected override SendNotificationCommand BuildCommand(WarrantyRegisteredEvent evt, Guid notificationId)
    {
        return new SendNotificationCommand(
            NotificationId: notificationId,
            Channel: NotificationChannels.Email,
            Recipient: evt.CustomerId.ToString(),
            Subject: "Warranty registered",
            Body: $"Your warranty {evt.WarrantyId} has been registered and is valid until {evt.ExpiresAt:yyyy-MM-dd}.");
    }
}

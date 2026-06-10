using Confluent.SchemaRegistry;
using NotificationOrchestrator.Service.Common.Messaging.Const;
using NotificationOrchestrator.Service.Contracts.Commands;
using NotificationOrchestrator.Service.Contracts.Events;

namespace NotificationOrchestrator.Service.Translators;

public sealed class WarrantyExpiredTranslator(ISchemaRegistryClient schemaRegistry)
    : WarrantyEventTranslatorBase<WarrantyExpiredEvent>(schemaRegistry)
{
    public override string EventType => EventTypes.WarrantyExpired;

    protected override SendNotificationCommand BuildCommand(WarrantyExpiredEvent evt, Guid notificationId)
    {
        return new SendNotificationCommand(
            NotificationId: notificationId,
            Channel: NotificationChannels.Email,
            Recipient: evt.CustomerId.ToString(),
            Subject: "Warranty expired",
            Body: $"Your warranty {evt.WarrantyId} expired on {evt.ExpiresAt:yyyy-MM-dd}.");
    }
}

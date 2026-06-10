using NotificationOrchestrator.Service.Contracts.Commands;

namespace NotificationOrchestrator.Service.Translators;

public interface IWarrantyEventTranslator
{
    string EventType { get; }
    Task<SendNotificationCommand> TranslateAsync(byte[] payload, string sourceTopic, string sourceMessageId);
}

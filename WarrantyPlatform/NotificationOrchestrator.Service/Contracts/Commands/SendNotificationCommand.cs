namespace NotificationOrchestrator.Service.Contracts.Commands;

public sealed record SendNotificationCommand(
    Guid NotificationId,
    string Channel,
    string Recipient,
    string Subject,
    string Body);

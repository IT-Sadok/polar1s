namespace NotificationOrchestrator.Service.Common.Messaging.Const;

public static class Topics
{
    public const string WarrantyEvents = "warranty-events";
    public const string NotificationCommands = "notification-commands";
}

public static class EventTypes
{
    public const string WarrantyRegistered = "warranty.registered.v1";
}

public static class CommandTypes
{
    public const string SendNotification = "notification.send.v1";
}

public static class EventSources
{
    public const string NotificationOrchestrator = "notification-orchestrator";
}

public static class NotificationChannels
{
    public const string Email = nameof(Email);
}

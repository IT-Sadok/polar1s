namespace Notification.Service.Entities;

public class InboxMessageEntity
{
    public Guid MessageId { get; set; }
    public string Type { get; set; } = string.Empty;
    public DateTime ReceivedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
}

namespace Warranty.Service.Common.Messaging.Serialization;

public interface IOutboxEventSerializer
{
    string EventType { get; }
    Task<byte[]> SerializeAsync(string payload, string topic);
}

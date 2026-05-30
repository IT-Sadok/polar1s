namespace Warranty.Service.Common.Messaging;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent payload, EventMetadata metadata, CancellationToken ct) where TEvent : class;
}

using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema.NewtonsoftJson.Generation;
using NotificationOrchestrator.Service.Contracts.Commands;

namespace NotificationOrchestrator.Service.Translators;

public abstract class WarrantyEventTranslatorBase<T> : IWarrantyEventTranslator
    where T : class
{
    private readonly JsonDeserializer<T> _deserializer;

    protected WarrantyEventTranslatorBase(ISchemaRegistryClient schemaRegistry)
    {
        var schemaGeneratorSettings = new NewtonsoftJsonSchemaGeneratorSettings
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        };
        _deserializer = new JsonDeserializer<T>(
            schemaRegistry,
            config: null,
            jsonSchemaGeneratorSettings: schemaGeneratorSettings);
    }

    public abstract string EventType { get; }

    public async Task<SendNotificationCommand> TranslateAsync(byte[] payload, string sourceTopic, string sourceMessageId)
    {
        var ctx = new SerializationContext(MessageComponentType.Value, sourceTopic);
        var evt = await _deserializer.DeserializeAsync(payload, isNull: false, ctx);

        var notificationId = Guid.TryParse(sourceMessageId, out var id) ? id : Guid.NewGuid();

        return BuildCommand(evt, notificationId);
    }

    protected abstract SendNotificationCommand BuildCommand(T evt, Guid notificationId);
}

using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema.NewtonsoftJson.Generation;
using System.Text.Json;

namespace Warranty.Service.Common.Messaging.Serialization;

public abstract class OutboxEventSerializerBase<T> : IOutboxEventSerializer
    where T : class
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private readonly JsonSerializer<T> _serializer;

    protected OutboxEventSerializerBase(ISchemaRegistryClient schemaRegistry)
    {
        var config = new JsonSerializerConfig
        {
            SubjectNameStrategy = SubjectNameStrategy.TopicRecord,
            AutoRegisterSchemas = false,
            UseLatestVersion = true,
        };
        var schemaGeneratorSettings = new NewtonsoftJsonSchemaGeneratorSettings
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        };
        _serializer = new(schemaRegistry, config, schemaGeneratorSettings);
    }

    public abstract string EventType { get; }

    public async Task<byte[]> SerializeAsync(string payload, string topic)
    {
        var evt = System.Text.Json.JsonSerializer.Deserialize<T>(payload, JsonOptions)!;
        var context = new SerializationContext(MessageComponentType.Value, topic);

        return await _serializer.SerializeAsync(evt, context);
    }
}

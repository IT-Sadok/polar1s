using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema.NewtonsoftJson.Generation;
using NotificationOrchestrator.Service.Common.Configuration;
using NotificationOrchestrator.Service.Contracts.Commands;
using NotificationOrchestrator.Service.Workers;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOptions<KafkaOptions>()
    .Bind(builder.Configuration.GetSection(KafkaOptions.SectionName));

builder.Services.AddSingleton<ISchemaRegistryClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<KafkaOptions>>().Value;
    return new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = options.SchemaRegistryUrl });
});

builder.Services.AddSingleton(sp =>
{
    var options = sp.GetRequiredService<IOptions<KafkaOptions>>().Value;
    var config = new ProducerConfig
    {
        BootstrapServers = options.BootstrapServers,
        Acks = options.Producers.NotificationCommands.Acks,
        EnableIdempotence = true,
        MessageSendMaxRetries = 3,
    };
    return new ProducerBuilder<string, byte[]>(config).Build();
});

builder.Services.AddSingleton<IAsyncSerializer<SendNotificationCommand>>(sp =>
{
    var schemaRegistry = sp.GetRequiredService<ISchemaRegistryClient>();
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
    return new JsonSerializer<SendNotificationCommand>(schemaRegistry, config, schemaGeneratorSettings);
});

builder.Services.AddHostedService<WarrantyEventsTranslator>();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapHealthChecks("/health");

app.Run();

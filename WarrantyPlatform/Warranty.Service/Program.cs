using Confluent.Kafka;
using Confluent.SchemaRegistry;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Warranty.Service.Common.Configuration;
using Warranty.Service.Common.Extensions;
using Warranty.Service.Common.Messaging;
using Warranty.Service.Common.Messaging.Serialization;
using Warranty.Service.Data;
using Warranty.Service.Endpoints;
using Warranty.Service.Jobs;
using Warranty.Service.Services;
using Warranty.Service.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOptions<GeneralDatabaseOptions>()
    .Bind(builder.Configuration.GetSection(GeneralDatabaseOptions.SectionName));

builder.Services
    .AddOptions<KafkaOptions>()
    .Bind(builder.Configuration.GetSection(KafkaOptions.SectionName));

builder.Services.AddOpenApi();

builder.Services.AddDbContext<WarrantyDbContext>((sp, options) =>
{
    var db = sp.GetRequiredService<IOptions<GeneralDatabaseOptions>>().Value;
    options.UseNpgsql(db.ConnectionString);

    if (builder.Environment.IsDevelopment())
    {
        options
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information);
    }
});

builder.Services.AddScoped<IWarrantyService, WarrantyService>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddScoped<IEventPublisher, OutboxEventPublisher>();

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
        Acks = options.Producers.WarrantyEvents.Acks,
        EnableIdempotence = true,
        MessageSendMaxRetries = 3,
    };
    return new ProducerBuilder<string, byte[]>(config).Build();
});

builder.Services.AddSingleton<IOutboxEventSerializer, WarrantyRegisteredOutboxSerializer>();
builder.Services.AddSingleton<IOutboxEventSerializer, WarrantyExpiredOutboxSerializer>();

builder.Services.AddHostedService<OutboxRelayJob>();
builder.Services.AddHostedService<WarrantyExpirySweepJob>();

var app = builder.Build();

app.MapDevelopmentApiDocs();
await app.MigrateAndSeedDatabaseAsync();

app.UseHttpsRedirection();

app.MapWarrantyEndpoints();

app.Run();

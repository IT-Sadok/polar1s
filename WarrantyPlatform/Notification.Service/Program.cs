using Confluent.SchemaRegistry;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Notification.Service.Common.Configuration;
using Notification.Service.Common.Extensions;
using Notification.Service.Consumers;
using Notification.Service.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOptions<GeneralDatabaseOptions>()
    .Bind(builder.Configuration.GetSection(GeneralDatabaseOptions.SectionName));

builder.Services
    .AddOptions<KafkaOptions>()
    .Bind(builder.Configuration.GetSection(KafkaOptions.SectionName));

builder.Services.AddDbContext<NotificationDbContext>((sp, options) =>
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

builder.Services.AddSingleton<ISchemaRegistryClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<KafkaOptions>>().Value;
    return new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = options.SchemaRegistryUrl });
});

builder.Services.AddHostedService<NotificationCommandsConsumer>();

var app = builder.Build();

await app.MigrateDatabaseAsync();

app.UseHttpsRedirection();

app.Run();

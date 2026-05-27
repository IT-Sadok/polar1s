using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Warranty.Service.Common.Configuration;
using Warranty.Service.Common.Extensions;
using Warranty.Service.Data;
using Warranty.Service.Endpoints;
using Warranty.Service.Services;
using Warranty.Service.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOptions<DatabaseOptions>()
    .Bind(builder.Configuration.GetSection(DatabaseOptions.SectionName));

builder.Services.AddOpenApi();

builder.Services.AddDbContext<WarrantyDbContext>((sp, options) =>
{
    var db = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
    options.UseNpgsql(db.ConnectionString);

    if (builder.Environment.IsDevelopment())
    {
        options
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information);
    }
});

builder.Services.AddScoped<IWarrantyService, WarrantyService>();

var app = builder.Build();

app.MapDevelopmentApiDocs();
await app.MigrateAndSeedDatabaseAsync();

app.UseHttpsRedirection();

app.MapWarrantyEndpoints();

app.Run();

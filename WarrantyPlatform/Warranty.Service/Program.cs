using FluentValidation;
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
    .AddOptions<GeneralDatabaseOptions>()
    .Bind(builder.Configuration.GetSection(GeneralDatabaseOptions.SectionName));

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

var app = builder.Build();

app.MapDevelopmentApiDocs();
await app.MigrateAndSeedDatabaseAsync();

app.UseHttpsRedirection();

app.MapWarrantyEndpoints();

app.Run();

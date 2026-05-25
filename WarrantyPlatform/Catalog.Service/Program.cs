using Catalog.Service.Common.Configuration;
using Catalog.Service.Common.Extensions;
using Catalog.Service.Common.Filters;
using Catalog.Service.Data;
using Catalog.Service.Services;
using Catalog.Service.Services.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOptions<DatabaseOptions>()
    .Bind(builder.Configuration.GetSection(DatabaseOptions.SectionName));
builder.Services
    .AddOptions<CacheOptions>()
    .Bind(builder.Configuration.GetSection(CacheOptions.SectionName));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());
builder.Services.AddScoped<ValidationFilter>();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<CatalogDbContext>((sp, options) =>
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

var cacheConfig = builder.Configuration
    .GetSection(CacheOptions.SectionName)
    .Get<CacheOptions>()!;

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = cacheConfig.ConnectionString;
    options.InstanceName = cacheConfig.InstanceName;
});

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IReportsService, ReportsService>();

var app = builder.Build();

app.MapDevelopmentApiDocs();
await app.MigrateAndSeedDatabaseAsync();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

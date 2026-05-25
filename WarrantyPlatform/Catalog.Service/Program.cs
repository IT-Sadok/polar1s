using Catalog.Service.Common.Extensions;
using Catalog.Service.Common.Filters;
using Catalog.Service.Data;
using Catalog.Service.Services;
using Catalog.Service.Services.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());
builder.Services.AddScoped<ValidationFilter>();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<CatalogDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Catalog"));

    if (builder.Environment.IsDevelopment())
    {
        options
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, LogLevel.Information);
    }
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "catalog:";
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

using Catalog.Service.Data;
using Catalog.Service.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace Catalog.Service.Common.Extensions;

public static class WebApplicationExtensions
{
    public static void MapDevelopmentApiDocs(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        app.MapOpenApi();
        app.MapScalarApiReference(options =>
            options.WithTitle("Catalog API")
                   .WithTheme(ScalarTheme.DeepSpace));
    }

    public static async Task MigrateAndSeedDatabaseAsync(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CatalogDbContext>();

        await db.Database.MigrateAsync();
        await CatalogSeeder.SeedAsync(db);
    }
}

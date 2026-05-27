using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Warranty.Service.Data;
using Warranty.Service.Data.Seed;

namespace Warranty.Service.Common.Extensions;

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
            options.WithTitle("Warranty API")
                   .WithTheme(ScalarTheme.DeepSpace));
    }

    public static async Task MigrateAndSeedDatabaseAsync(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<WarrantyDbContext>();

        await db.Database.MigrateAsync();
        await WarrantySeeder.SeedAsync(db);
    }
}

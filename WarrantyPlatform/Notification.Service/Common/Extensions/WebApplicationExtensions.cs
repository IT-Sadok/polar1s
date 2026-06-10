using Microsoft.EntityFrameworkCore;
using Notification.Service.Data;

namespace Notification.Service.Common.Extensions;

public static class WebApplicationExtensions
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return;
        }

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();

        await db.Database.MigrateAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using Warranty.Service.Entities;

namespace Warranty.Service.Data;

public class WarrantyDbContext(DbContextOptions<WarrantyDbContext> options) : DbContext(options)
{
    public DbSet<WarrantyEntity> Warranties => Set<WarrantyEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WarrantyDbContext).Assembly);
    }
}

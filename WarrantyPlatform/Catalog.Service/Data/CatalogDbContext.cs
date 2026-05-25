using Catalog.Service.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Data;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    public DbSet<ProductEntity> Products => Set<ProductEntity>();
    public DbSet<BrandEntity> Brands => Set<BrandEntity>();
    public DbSet<SupplierEntity> Suppliers => Set<SupplierEntity>();
    public DbSet<ProductImageEntity> ProductImages => Set<ProductImageEntity>();
    public DbSet<ProductSupplierEntity> ProductSuppliers => Set<ProductSupplierEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }
}

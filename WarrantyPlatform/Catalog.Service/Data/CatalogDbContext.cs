using Catalog.Service.Domain;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Service.Data;

public class CatalogDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<ProductSupplier> ProductSuppliers => Set<ProductSupplier>();

    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
    }
}

using Catalog.Service.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Service.Data.Configs;

public class ProductSupplierConfiguration : IEntityTypeConfiguration<ProductSupplierEntity>
{
    public void Configure(EntityTypeBuilder<ProductSupplierEntity> builder)
    {
        builder.HasKey(ps => new { ps.ProductId, ps.SupplierId });

        builder.Property(ps => ps.UnitCost)
            .HasPrecision(12, 2);

        builder.HasOne(ps => ps.Product)
            .WithMany(p => p.ProductSuppliers)
            .HasForeignKey(ps => ps.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ps => ps.Supplier)
            .WithMany(s => s.ProductSuppliers)
            .HasForeignKey(ps => ps.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(ps => ps.CreatedAt)
            .HasDefaultValueSql("now()");

        builder.HasIndex(ps => ps.ProductId)
            .IncludeProperties(ps => ps.UnitCost);
    }
}

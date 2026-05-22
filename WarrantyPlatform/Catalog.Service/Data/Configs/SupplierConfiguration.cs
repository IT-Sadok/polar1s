using Catalog.Service.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Service.Data.Configs;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.ContactEmail)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(s => s.CreatedAt)
            .HasDefaultValueSql("now()");

        builder.HasIndex(s => s.ContactEmail).IsUnique();
    }
}

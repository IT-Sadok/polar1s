using Catalog.Service.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Service.Data.Configs;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.HasKey(b => b.Id);

        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Country)
            .HasMaxLength(100);

        builder.Property(b => b.CreatedAt)
            .HasDefaultValueSql("now()");

        builder.HasIndex(b => b.Name).IsUnique();
    }
}

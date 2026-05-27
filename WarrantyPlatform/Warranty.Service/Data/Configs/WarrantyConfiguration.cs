using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warranty.Service.Entities;

namespace Warranty.Service.Data.Configs;

public class WarrantyConfiguration : IEntityTypeConfiguration<WarrantyEntity>
{
    public void Configure(EntityTypeBuilder<WarrantyEntity> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.CreatedAt)
            .HasDefaultValueSql("now()");

        builder.HasIndex(w => new { w.CustomerId, w.CreatedAt, w.Id })
            .IsDescending(false, true, true);
    }
}

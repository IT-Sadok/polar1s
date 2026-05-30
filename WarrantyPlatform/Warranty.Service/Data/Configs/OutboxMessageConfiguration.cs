using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warranty.Service.Entities;

namespace Warranty.Service.Data.Configs;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessageEntity>
{
    public void Configure(EntityTypeBuilder<OutboxMessageEntity> builder)
    {
        builder.HasKey(o => o.Id);

        builder.HasIndex(o => o.MessageId)
            .IsUnique();

        builder.Property(o => o.AggregateType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(o => o.Topic)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(o => o.Type)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(o => o.PartitionKey)
            .HasMaxLength(100);

        builder.Property(o => o.Payload)
            .HasColumnType("jsonb")
            .IsRequired();

        builder.Property(o => o.CreatedAt)
            .HasDefaultValueSql("now()");

        builder.HasIndex(o => o.CreatedAt)
            .HasFilter("\"ProcessedAt\" IS NULL")
            .HasDatabaseName("ix_outbox_pending");
    }
}

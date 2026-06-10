using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Service.Entities;

namespace Notification.Service.Data.Configs;

public class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessageEntity>
{
    public void Configure(EntityTypeBuilder<InboxMessageEntity> builder)
    {
        builder.HasKey(i => i.MessageId);

        builder.Property(i => i.Type)
            .HasMaxLength(100);

        builder.Property(i => i.ReceivedAt)
            .HasDefaultValueSql("now()");
    }
}

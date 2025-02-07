using Audit.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.Infrastructure.Persistence.EntityFramework.Configurations;

public class AuditRecordMetadataConfiguration : IEntityTypeConfiguration<AuditRecordMetadata>
{
    public void Configure(EntityTypeBuilder<AuditRecordMetadata> builder)
    {
        builder.ToTable(nameof(AuditDbContext.AuditRecordMetadata));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.AuditRecordId);
        builder.Property(x => x.PropertyName);
        builder.Property(x => x.OriginalValue);
        builder.Property(x => x.UpdatedValue);
    }
}
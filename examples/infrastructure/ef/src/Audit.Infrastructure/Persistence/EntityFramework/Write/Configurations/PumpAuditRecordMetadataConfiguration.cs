using Audit.Domain.Abstraction.Model.Audit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.Infrastructure.Persistence.EntityFramework.Write.Configurations;

public class PumpAuditRecordMetadataConfiguration : IEntityTypeConfiguration<AuditRecordMetadata>
{
    public void Configure(EntityTypeBuilder<AuditRecordMetadata> builder)
    {
        builder.ToTable(nameof(DbContext.PumpAuditRecordMetadata));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.AuditRecordId).HasColumnName("PumpAuditRecordId");
        builder.Property(x => x.PropertyName);
        builder.Property(x => x.OriginalValue);
        builder.Property(x => x.UpdatedValue);
    }
}
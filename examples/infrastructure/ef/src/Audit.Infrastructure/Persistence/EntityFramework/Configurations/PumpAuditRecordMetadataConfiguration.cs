using Audit.Domain.Abstraction.Model.Audit;
using Audit.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.Infrastructure.Persistence.EntityFramework.Configurations;

public class PumpAuditRecordMetadataConfiguration : IEntityTypeConfiguration<AuditRecordMetadata<PumpAuditRecord>>
{
    public void Configure(EntityTypeBuilder<AuditRecordMetadata<PumpAuditRecord>> builder)
    {
        builder.ToTable(nameof(DbContext.PumpAuditRecordMetadata));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.AuditRecordId).HasColumnName("PumpAuditRecordId");
        builder.Property(x => x.PropertyName);
        builder.Property(x => x.OriginalValue);
        builder.Property(x => x.UpdatedValue);
    }
}
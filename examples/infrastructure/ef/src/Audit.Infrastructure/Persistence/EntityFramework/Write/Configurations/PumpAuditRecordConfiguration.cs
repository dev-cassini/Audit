using Audit.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.Infrastructure.Persistence.EntityFramework.Write.Configurations;

public class PumpAuditRecordConfiguration : IEntityTypeConfiguration<PumpAuditRecord>
{
    public void Configure(EntityTypeBuilder<PumpAuditRecord> builder)
    {
        builder.ToTable(nameof(DbContext.PumpAuditRecords));
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Timestamp);
        builder.Property(x => x.VehicleId);
        builder.Property(x => x.LaneId);
        
        builder
            .HasMany(x => x.Metadata)
            .WithOne()
            .HasForeignKey(x => x.AuditRecordId);
    }
}
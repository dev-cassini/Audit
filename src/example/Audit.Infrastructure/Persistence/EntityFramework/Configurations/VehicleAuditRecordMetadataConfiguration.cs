using Audit.Domain.Model.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.Infrastructure.Persistence.EntityFramework.Configurations;

public class VehicleAuditRecordMetadataConfiguration : IEntityTypeConfiguration<VehicleAuditRecordMetadata>
{
    public void Configure(EntityTypeBuilder<VehicleAuditRecordMetadata> builder)
    {
        builder.ToTable(nameof(AuditDbContext.VehicleAuditRecordMetadata));

        builder.HasKey(x => x.Id);
        builder.Property(x => x.PropertyName);
        builder.Property(x => x.OriginalValue);
        builder.Property(x => x.UpdatedValue);
    }
}
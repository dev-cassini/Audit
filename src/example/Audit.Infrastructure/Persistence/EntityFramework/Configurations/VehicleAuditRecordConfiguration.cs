using Audit.Domain.Model.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.Infrastructure.Persistence.EntityFramework.Configurations;

public class VehicleAuditRecordConfiguration : IEntityTypeConfiguration<VehicleAuditRecord>
{
    public void Configure(EntityTypeBuilder<VehicleAuditRecord> builder)
    {
        builder.ToTable(nameof(AuditDbContext.VehicleAuditRecords));
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Timestamp);
        builder.Property(x => x.Type);
        builder.Property(x => x.FuelType);
        builder.Property(x => x.FuelLevel);
        builder.Property(x => x.TankCapacity);
        
        builder
            .HasOne(x => x.Vehicle)
            .WithMany(x => x.AuditRecords)
            .HasForeignKey(x => x.VehicleId);
        
        builder
            .HasMany(x => x.Metadata)
            .WithOne()
            .HasForeignKey(x => x.AuditRecordId);
    }
}
using Audit.Domain.Model.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.Infrastructure.Persistence.EntityFramework.Configurations;

public class VehicleAuditConfiguration : IEntityTypeConfiguration<VehicleAudit>
{
    public void Configure(EntityTypeBuilder<VehicleAudit> builder)
    {
        builder.ToTable(nameof(AuditDbContext.VehiclesAudit));
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Type);
        builder.Property(x => x.FuelType);
        builder.Property(x => x.FuelLevel);
        builder.Property(x => x.TankCapacity);

        builder
            .HasOne(x => x.Vehicle)
            .WithMany(x => x.AuditRecords)
            .HasForeignKey(x => x.VehicleId);
    }
}
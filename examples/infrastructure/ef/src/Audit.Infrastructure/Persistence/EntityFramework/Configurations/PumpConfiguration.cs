using Audit.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.Infrastructure.Persistence.EntityFramework.Configurations;

public class PumpConfiguration : IEntityTypeConfiguration<Pump>
{
    public void Configure(EntityTypeBuilder<Pump> builder)
    {
        builder.ToTable(nameof(AuditDbContext.Pumps));
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.VehicleId);
        builder.Property(x => x.LaneId);

        builder
            .HasOne(x => x.Vehicle)
            .WithOne()
            .HasForeignKey<Pump>(x => x.VehicleId)
            .IsRequired(false);
    }
}
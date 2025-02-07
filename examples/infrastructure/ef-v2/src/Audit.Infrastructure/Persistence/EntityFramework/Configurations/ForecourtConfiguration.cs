using Audit.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.Infrastructure.Persistence.EntityFramework.Configurations;

public class ForecourtConfiguration : IEntityTypeConfiguration<Forecourt>
{
    public void Configure(EntityTypeBuilder<Forecourt> builder)
    {
        builder.ToTable(nameof(AuditDbContext.Forecourts));

        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Lanes)
            .WithOne()
            .HasForeignKey(x => x.ForecourtId);
    }
}
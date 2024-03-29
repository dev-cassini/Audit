using Audit.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.Infrastructure.Persistence.EntityFramework.Write.Configurations;

public class ForecourtConfiguration : IEntityTypeConfiguration<Forecourt>
{
    public void Configure(EntityTypeBuilder<Forecourt> builder)
    {
        builder.ToTable(nameof(DbContext.Forecourts));

        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Lanes)
            .WithOne()
            .HasForeignKey(x => x.ForecourtId);
    }
}
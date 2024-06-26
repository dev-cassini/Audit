using Audit.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.Infrastructure.Persistence.EntityFramework.Write.Configurations;

public class LaneConfiguration : IEntityTypeConfiguration<Lane>
{
    public void Configure(EntityTypeBuilder<Lane> builder)
    {
        builder.ToTable(nameof(DbContext.Lanes));

        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.Pumps)
            .WithOne(x => x.Lane)
            .HasForeignKey(x => x.LaneId);
    }
}
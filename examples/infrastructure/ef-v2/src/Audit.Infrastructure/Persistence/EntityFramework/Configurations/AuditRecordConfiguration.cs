using Audit.Domain.Model;
using Audit.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Audit.Infrastructure.Persistence.EntityFramework.Configurations;

public class AuditRecordConfiguration : IEntityTypeConfiguration<AuditRecord>
{
    public void Configure(EntityTypeBuilder<AuditRecord> builder)
    {
        builder.ToTable(nameof(AuditDbContext.AuditRecords));
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ResourceId);
        builder.Property(x => x.TimestampUtc);
        
        builder.OwnsOne<Actor>(x => x.Actor);
        builder
            .HasMany(x => x.Metadata)
            .WithOne()
            .HasForeignKey(x => x.AuditRecordId);
    }
}
using System.Reflection;
using Audit.Domain.Abstraction.Model.Audit;
using Audit.Domain.Model.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Audit.Infrastructure.Persistence.EntityFramework;

public class AuditDbContext : DbContext
{
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    public DbSet<VehicleAuditRecord> VehicleAuditRecords { get; set; } = null!;
    public DbSet<AuditRecordMetadata<VehicleAuditRecord>> VehicleAuditRecordMetadata { get; set; } = null!;
    
    public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties().Where(x => x.IsPrimaryKey()))
            {
                property.ValueGenerated = ValueGenerated.Never;
            }
        }
    }
}
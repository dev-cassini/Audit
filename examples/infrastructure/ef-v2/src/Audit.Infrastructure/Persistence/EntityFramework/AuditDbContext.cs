using System.Reflection;
using Audit.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Audit.Infrastructure.Persistence.EntityFramework;

public class AuditDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Forecourt> Forecourts { get; set; } = null!;
    public DbSet<Lane> Lanes { get; set; } = null!;
    public DbSet<Pump> Pumps { get; set; } = null!;
    public DbSet<AuditRecord> AuditRecords { get; set; } = null!;
    public DbSet<AuditRecordMetadata> AuditRecordMetadata { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    
    protected AuditDbContext() { }
    
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
using System.Reflection;
using Audit.Domain.Abstraction.Model.Audit;
using Audit.Domain.Abstraction.Tooling;
using Audit.Domain.Model;
using Audit.Infrastructure.Persistence.EntityFramework.Interceptors.SaveChanges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Audit.Infrastructure.Persistence.EntityFramework.Write;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public DbSet<Forecourt> Forecourts { get; set; } = null!;
    public DbSet<Lane> Lanes { get; set; } = null!;
    public DbSet<Pump> Pumps { get; set; } = null!;
    public DbSet<PumpAuditRecord> PumpAuditRecords { get; set; } = null!;
    public DbSet<AuditRecordMetadata> PumpAuditRecordMetadata { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    
    protected DbContext(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    
    public DbContext(DbContextOptions<DbContext> options, IDateTimeProvider dateTimeProvider) 
        : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .AddInterceptors(
                new CreateInitialAuditInterceptor<PumpAuditRecord>(_dateTimeProvider),
                new CreateUpdateAuditInterceptor<PumpAuditRecord>(_dateTimeProvider));
}
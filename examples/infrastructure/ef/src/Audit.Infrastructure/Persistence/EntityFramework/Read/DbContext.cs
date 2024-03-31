using System.Reflection;
using Audit.Domain.Abstraction.Model.Audit;
using Audit.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Audit.Infrastructure.Persistence.EntityFramework.Read;

public class DbContext(DbContextOptions<DbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    private DbSet<Forecourt> Forecourts { get; set; } = null!;
    public IQueryable<Forecourt> ForecourtsView => Forecourts.AsNoTracking();
    
    private DbSet<Lane> Lanes { get; set; } = null!;
    public IQueryable<Lane> LanesView => Lanes.AsNoTracking();
    
    private DbSet<Pump> Pumps { get; set; } = null!;
    public IQueryable<Pump> PumpsView => Pumps.AsNoTracking();
    
    private DbSet<PumpAuditRecord> PumpAuditRecords { get; set; } = null!;
    public IQueryable<PumpAuditRecord> PumpAuditRecordsView => PumpAuditRecords.AsNoTracking();
    
    private DbSet<AuditRecordMetadata> PumpAuditRecordMetadata { get; set; } = null!;
    public IQueryable<AuditRecordMetadata> PumpAuditRecordMetadataView => PumpAuditRecordMetadata.AsNoTracking();
    
    private DbSet<Vehicle> Vehicles { get; set; } = null!;
    public IQueryable<Vehicle> VehiclesView => Vehicles.AsNoTracking();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
using Audit.Domain.Abstraction.Model.Audit;
using Audit.Domain.Model.Vehicles;

namespace Audit.Domain.Model;

public class Pump : Abstraction.Model.Pump, IAuditable<PumpAuditRecord>
{
    public Vehicle? Vehicle { get; private set; }
    
    private readonly List<PumpAuditRecord> _auditRecords = new();
    public IEnumerable<PumpAuditRecord> AuditRecords => _auditRecords.Where(x => x.Metadata.Any());
    
    public Pump(Guid id, Lane lane) : base(id, lane) { }
    
    public void Filling(Vehicle vehicle)
    {
        VehicleId = vehicle.Id;
        Vehicle = vehicle;
    }
    
    public void Release()
    {
        if (Vehicle is null)
        {
            return;
        }
        
        Vehicle.FuelLevel = Vehicle.TankCapacity;
        Vehicle.AddAuditRecord();
        VehicleId = null;
        Vehicle = null;
    }
    
    public void AddAuditRecord()
    {
        var auditRecord = new PumpAuditRecord(this);
        _auditRecords.Add(auditRecord);
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    private Pump() { }
    #endregion
}
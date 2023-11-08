using Audit.Domain.Abstraction.Enums;

namespace Audit.Domain.Model.Vehicles;

public abstract class Vehicle : Abstraction.Model.Vehicle, IAuditable
{
    private readonly List<VehicleAudit> _auditRecords = new();
    public IEnumerable<VehicleAudit> AuditRecords => _auditRecords.AsReadOnly();
    
    protected Vehicle(
        Guid id, 
        VehicleType type, 
        FuelType fuelType, 
        int fuelLevel, 
        int tankCapacity)
    : base(id, type, fuelType, fuelLevel, tankCapacity) { }

    public void AddAuditRecord()
    {
        var auditRecord = new VehicleAudit(this);
        _auditRecords.Add(auditRecord);
    }
}
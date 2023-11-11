using Audit.Domain.Abstraction.Enums;
using Audit.Domain.Model.Audit;

namespace Audit.Domain.Model.Vehicles;

public abstract class Vehicle : Abstraction.Model.Vehicle, IAuditable<VehicleAuditRecord>
{
    private readonly List<VehicleAuditRecord> _auditRecords = new();
    public IEnumerable<VehicleAuditRecord> AuditRecords => _auditRecords.Where(x => x.Metadata.Any());

    protected Vehicle(
        Guid id,
        VehicleType type,
        FuelType fuelType,
        int fuelLevel,
        int tankCapacity)
        : base(id, type, fuelType, fuelLevel, tankCapacity)
    {
        AddAuditRecord();
    }

    public void AddAuditRecord()
    {
        var auditRecord = new VehicleAuditRecord(this);
        _auditRecords.Add(auditRecord);
    }
}
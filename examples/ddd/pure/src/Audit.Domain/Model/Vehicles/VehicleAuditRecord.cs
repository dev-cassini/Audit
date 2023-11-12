using Audit.Domain.Abstraction.Model.Audit;

namespace Audit.Domain.Model.Vehicles;

public class VehicleAuditRecord : Abstraction.Model.Vehicle, IAuditRecord
{
    public sealed override Guid Id { get; } = Guid.NewGuid();
    public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;
    public IEnumerable<AuditRecordMetadata<VehicleAuditRecord>> Metadata { get; } = null!;
    public Guid VehicleId { get; }
    public Vehicle Vehicle { get; } = null!;

    public VehicleAuditRecord(Vehicle vehicle) 
        : base(
            vehicle.Type,
            vehicle.FuelType, 
            vehicle.FuelLevel, 
            vehicle.TankCapacity)
    {
        VehicleId = vehicle.Id;
        Vehicle = vehicle;
        Metadata = vehicle.ToAuditRecordMetadata(this);
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    protected VehicleAuditRecord() { }
    #endregion
}
using Audit.Domain.Exceptions;

namespace Audit.Domain.Model.Vehicles;

public class VehicleAuditRecord : Abstraction.Model.Vehicle
{
    public sealed override Guid Id { get; } = Guid.NewGuid();
    public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;
    public IEnumerable<VehicleAuditRecordMetadata> Metadata { get; } = null!;
    public Guid VehicleId { get; }
    public Vehicle Vehicle { get; } = null!;

    public VehicleAuditRecord(Vehicle vehicle) 
        : base(
            vehicle.Id, 
            vehicle.Type,
            vehicle.FuelType, 
            vehicle.FuelLevel, 
            vehicle.TankCapacity)
    {
        var originalVehicle = vehicle.AuditRecords.MaxBy(x => x.Timestamp);
        var auditRecordMetadata = new List<VehicleAuditRecordMetadata>();
        foreach (var property in vehicle.GetType().GetProperties()
                     .Where(x => x.SetMethod is not null)
                     .Where(x => x.PropertyType.IsClass is false || x.PropertyType == typeof(string)))
        {
            var originalValue = property.GetValue(originalVehicle);
            var updatedValue = property.GetValue(vehicle);
            if (originalValue is null && updatedValue is null) continue;
            
            if (originalValue is null || 
                updatedValue is null || 
                originalValue.Equals(updatedValue) is false)
            {
                auditRecordMetadata.Add(new VehicleAuditRecordMetadata(
                    this,
                    property.Name,
                    originalValue?.ToString(),
                    updatedValue?.ToString()));
            }
        }

        if (auditRecordMetadata.Any() is false)
        {
            throw new NoChangesToAuditException(this, vehicle.Id);
        }

        Metadata = auditRecordMetadata;
        VehicleId = vehicle.Id;
        Vehicle = vehicle;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    protected VehicleAuditRecord() { }
    #endregion
}
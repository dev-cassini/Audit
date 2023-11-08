namespace Audit.Domain.Model.Vehicles;

public class VehicleAudit : Vehicle
{
    public sealed override Guid Id { get; } = Guid.NewGuid();
    public Guid VehicleId { get; }
    public Vehicle Vehicle { get; }

    public VehicleAudit(Vehicle vehicle) 
        : base(
            vehicle.Id, 
            vehicle.Type,
            vehicle.FuelType, 
            vehicle.FuelLevel, 
            vehicle.TankCapacity)
    {
        VehicleId = vehicle.Id;
        Vehicle = vehicle;
    }
}
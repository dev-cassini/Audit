namespace Audit.Domain.Model.Vehicles;

public class VehicleAudit : Abstraction.Model.Vehicle
{
    public sealed override Guid Id { get; } = Guid.NewGuid();
    public Guid VehicleId { get; }
    public Vehicle Vehicle { get; } = null!;

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
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    protected VehicleAudit() { }
    #endregion
}
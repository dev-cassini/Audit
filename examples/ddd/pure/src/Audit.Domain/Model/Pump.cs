using Audit.Domain.Model.Vehicles;

namespace Audit.Domain.Model;

public class Pump : Abstraction.Model.Pump
{
    public Vehicle? Vehicle { get; private set; }
    
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
}
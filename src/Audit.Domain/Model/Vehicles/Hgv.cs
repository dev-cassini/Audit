using Audit.Domain.Enums;

namespace Audit.Domain.Model.Vehicles;

public class Hgv : Vehicle
{
    protected Hgv(
        Guid id,
        FuelType fuelType, 
        int fuelLevel, 
        int tankCapacity) 
        : base(id, VehicleType.Hgv, fuelType, fuelLevel, tankCapacity)
    {
    }
}
using Audit.Domain.Abstraction.Enums;

namespace Audit.Domain.Model.Vehicles;

public class Hgv : Vehicle
{
    public Hgv(
        FuelType fuelType, 
        int fuelLevel, 
        int tankCapacity) 
        : base(VehicleType.Hgv, fuelType, fuelLevel, tankCapacity)
    {
    }
}
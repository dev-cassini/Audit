using Audit.Domain.Abstraction.Enums;

namespace Audit.Domain.Model.Vehicles;

public class Van : Vehicle
{
    public Van(
        FuelType fuelType, 
        int fuelLevel, 
        int tankCapacity) 
        : base(VehicleType.Van, fuelType, fuelLevel, tankCapacity)
    {
    }
}
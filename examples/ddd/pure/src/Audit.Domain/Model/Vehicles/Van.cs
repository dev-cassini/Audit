using Audit.Domain.Abstraction.Enums;

namespace Audit.Domain.Model.Vehicles;

public class Van : Vehicle
{
    public Van(
        Guid id,
        FuelType fuelType, 
        int fuelLevel, 
        int tankCapacity) 
        : base(id, VehicleType.Van, fuelType, fuelLevel, tankCapacity)
    {
    }
}
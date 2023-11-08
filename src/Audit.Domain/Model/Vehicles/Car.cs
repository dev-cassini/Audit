using Audit.Domain.Enums;

namespace Audit.Domain.Model.Vehicles;

public class Car : Vehicle
{
    protected Car(
        Guid id,
        FuelType fuelType, 
        int fuelLevel, 
        int tankCapacity) 
        : base(id, VehicleType.Car, fuelType, fuelLevel, tankCapacity)
    {
    }
}
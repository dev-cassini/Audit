using Audit.Domain.Abstraction.Enums;

namespace Audit.Domain.Model.Vehicles;

public class Car : Vehicle
{
    public Car(
        Guid id,
        FuelType fuelType, 
        int fuelLevel, 
        int tankCapacity) 
        : base(id, VehicleType.Car, fuelType, fuelLevel, tankCapacity)
    {
    }
}
using Audit.Domain.Abstraction.Enums;

namespace Audit.Domain.Model.Vehicles;

public class Car : Vehicle
{
    public Car(
        FuelType fuelType, 
        int fuelLevel, 
        int tankCapacity) 
        : base(VehicleType.Car, fuelType, fuelLevel, tankCapacity)
    {
    }
}
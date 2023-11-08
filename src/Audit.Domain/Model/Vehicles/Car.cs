using Audit.Domain.Abstraction.Enums;
using Audit.Domain.Abstraction.Model;

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
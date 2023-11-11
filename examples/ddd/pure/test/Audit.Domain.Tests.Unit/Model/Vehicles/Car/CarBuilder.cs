using Audit.Domain.Abstraction.Enums;

namespace Audit.Domain.Tests.Unit.Model.Vehicles.Car;

using Car = Domain.Model.Vehicles.Car;

public class CarBuilder
{
    public Car Build()
    {
        return new Car(FuelType.Diesel, 100, 150);
    }
}
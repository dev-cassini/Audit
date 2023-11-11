namespace Audit.Domain.Tests.Unit.Model.Vehicles;

using Car = Domain.Model.Vehicles.Car;
using Vehicle = Domain.Model.Vehicles.Vehicle;

[TestFixture]
public class CarTests
{
    [Test]
    public void CarBaseIsVehicle()
    {
        Assert.That(typeof(Car).IsSubclassOf(typeof(Vehicle)), Is.True);
    }
}
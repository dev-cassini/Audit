using Audit.Domain.Model.Vehicles;

namespace Audit.Domain.Tests.Unit.Model.Vehicles;

[TestFixture]
public class CarTests
{
    [Test]
    public void CarBaseIsVehicle()
    {
        Assert.That(typeof(Car).IsSubclassOf(typeof(Vehicle)), Is.True);
    }
}
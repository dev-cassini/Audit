namespace Audit.Domain.Tests.Unit.Model.Vehicles;

using Van = Domain.Model.Vehicles.Van;
using Vehicle = Domain.Model.Vehicles.Vehicle;

[TestFixture]
public class VanTests
{
    [Test]
    public void VanBaseIsVehicle()
    {
        Assert.That(typeof(Van).IsSubclassOf(typeof(Vehicle)), Is.True);
    }
}
using Audit.Domain.Model.Vehicles;

namespace Audit.Domain.Tests.Unit.Model.Vehicles;

[TestFixture]
public class VanTests
{
    [Test]
    public void VanBaseIsVehicle()
    {
        Assert.That(typeof(Van).IsSubclassOf(typeof(Vehicle)), Is.True);
    }
}
using Audit.Domain.Model.Vehicles;

namespace Audit.Domain.Tests.Unit.Model.Vehicles;

[TestFixture]
public class HgvTests
{
    [Test]
    public void HgvBaseIsVehicle()
    {
        Assert.That(typeof(Hgv).IsSubclassOf(typeof(Vehicle)), Is.True);
    }
}
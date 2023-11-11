namespace Audit.Domain.Tests.Unit.Model.Vehicles;

using Hgv = Domain.Model.Vehicles.Hgv;
using Vehicle = Domain.Model.Vehicles.Vehicle;

[TestFixture]
public class HgvTests
{
    [Test]
    public void HgvBaseIsVehicle()
    {
        Assert.That(typeof(Hgv).IsSubclassOf(typeof(Vehicle)), Is.True);
    }
}
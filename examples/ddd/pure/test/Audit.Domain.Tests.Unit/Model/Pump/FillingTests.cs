using Audit.Domain.Abstraction.Enums;
using Audit.Domain.Model;
using Audit.Domain.Model.Vehicles;

namespace Audit.Domain.Tests.Unit.Model.Pump;

[TestFixture]
public class FillingTests
{
    [Test]
    public void GivenPumpIsFree_WhenPumpIsFilling_ThenPumpIsAssignedToVehicle()
    {
        var car = new Car(FuelType.Diesel, 100, 150);
        var forecourt = new Forecourt(Guid.NewGuid());
        var lane = forecourt.AddLane();
        var pump = lane.AddPump();
        
        pump.Filling(car);
        
        Assert.That(pump.VehicleId, Is.EqualTo(car.Id));
        Assert.That(pump.Vehicle, Is.EqualTo(car));
    }
}
using Audit.Domain.Abstraction.Enums;
using Audit.Domain.Model;
using Audit.Domain.Model.Vehicles;

namespace Audit.Domain.Tests.Unit.Model.Pump;

[TestFixture]
public class ReleaseTests
{
    [Test]
    public void GivenPumpIsFree_WhenPumpIsReleased_ThenVehicleAuditRecordIsNotCreated()
    {
        var car = new Car(FuelType.Diesel, 100, 150);
        var forecourt = new Forecourt(Guid.NewGuid());
        var lane = forecourt.AddLane();
        var pump = lane.AddPump();
        
        Assert.That(car.AuditRecords.Count(), Is.EqualTo(1));
        
        pump.Release();
        
        Assert.That(car.AuditRecords.Count(), Is.EqualTo(1));
    }
    
    [Test]
    public void GivenPumpIsFilling_WhenPumpIsReleased_ThenVehicleFuelLevelIsUpdatedToTankCapacity()
    {
        var car = new Car(FuelType.Diesel, 100, 150);
        var forecourt = new Forecourt(Guid.NewGuid());
        var lane = forecourt.AddLane();
        var pump = lane.AddPump();
        
        pump.Filling(car);
        pump.Release();
        
        Assert.That(car.FuelLevel, Is.EqualTo(car.TankCapacity));
    }
    
    [Test]
    public void GivenPumpIsFilling_WhenPumpIsReleased_ThenVehicleAuditRecordIsCreated()
    {
        var car = new Car(FuelType.Diesel, 100, 150);
        var forecourt = new Forecourt(Guid.NewGuid());
        var lane = forecourt.AddLane();
        var pump = lane.AddPump();
        pump.Filling(car);
        
        Assert.That(car.AuditRecords.Count(), Is.EqualTo(1));
        
        pump.Release();

        Assert.That(car.AuditRecords.Count(), Is.EqualTo(2));
    }
    
    [Test]
    public void GivenPumpIsFilling_WhenPumpIsReleased_ThenAuditRecordMetadataIsCreatedForFuelLevel()
    {
        const int originalFuelLevel = 100;
        var car = new Car(FuelType.Diesel, originalFuelLevel, originalFuelLevel + 50);
        var forecourt = new Forecourt(Guid.NewGuid());
        var lane = forecourt.AddLane();
        var pump = lane.AddPump();
        
        pump.Filling(car);
        pump.Release();

        var auditRecord = car.AuditRecords.MaxBy(x => x.Timestamp);
        Assert.Multiple(() =>
        {
            Assert.That(auditRecord, Is.Not.Null);
            Assert.That(auditRecord!.Metadata.Count(), Is.EqualTo(1));

            var auditRecordMetadata = auditRecord.Metadata.Single();
            Assert.That(auditRecordMetadata.PropertyName, Is.EqualTo(nameof(Vehicle.FuelLevel)));
            Assert.That(auditRecordMetadata.OriginalValue, Is.EqualTo(originalFuelLevel.ToString()));
            Assert.That(auditRecordMetadata.UpdatedValue, Is.EqualTo(car.FuelLevel.ToString()));
        });
    }
    
    [Test]
    public void GivenPumpIsFilling_WhenPumpIsReleased_ThenVehicleIsUnassignedFromPump()
    {
        var car = new Car(FuelType.Diesel, 100, 150);
        var forecourt = new Forecourt(Guid.NewGuid());
        var lane = forecourt.AddLane();
        var pump = lane.AddPump();
        
        pump.Filling(car);
        pump.Release();
        
        Assert.That(pump.VehicleId, Is.Null);
        Assert.That(pump.Vehicle, Is.Null);
    }
}
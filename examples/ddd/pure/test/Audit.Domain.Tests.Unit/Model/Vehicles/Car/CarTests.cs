namespace Audit.Domain.Tests.Unit.Model.Vehicles.Car;

[TestFixture]
public class CarTests
{
    [Test]
    public void AuditRecordIsLinkedToCar()
    {
        var car = new CarBuilder().Build();
        Assert.That(car.AuditRecords.Single().VehicleId, Is.EqualTo(car.Id));
    }

    [Test]
    public void AuditRecordIsCopyOfCar()
    {
        var car = new CarBuilder().Build();
        
        Assert.Multiple(() =>
        {
            var auditRecord = car.AuditRecords.Single();
            Assert.That(auditRecord.Type, Is.EqualTo(car.Type));
            Assert.That(auditRecord.FuelType, Is.EqualTo(car.FuelType));
            Assert.That(auditRecord.FuelLevel, Is.EqualTo(car.FuelLevel));
            Assert.That(auditRecord.TankCapacity, Is.EqualTo(car.TankCapacity));
        });
    }
    
    [Test]
    public void NewCarIncludesOneInitialisationAuditRecord()
    {
        var car = new CarBuilder().Build();
        Assert.Multiple(() =>
        {
            Assert.That(car.AuditRecords.Count(), Is.EqualTo(1));
            
            var auditRecord = car.AuditRecords.Single();
            Assert.That(auditRecord.Metadata.Count(), Is.EqualTo(1));

            var auditRecordMetadata = auditRecord.Metadata.Single();
            Assert.That(auditRecordMetadata.PropertyName, Is.EqualTo("CreationDateTimeUtc"));
        });
    }
}
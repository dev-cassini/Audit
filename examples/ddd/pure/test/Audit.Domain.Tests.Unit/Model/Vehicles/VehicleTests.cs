using Audit.Domain.Abstraction.Enums;
using Audit.Domain.Model.Vehicles;

namespace Audit.Domain.Tests.Unit.Model.Vehicles;

[TestFixture]
public class VehicleTests
{
    [Test]
    public void AuditRecordIsLinkedToVehicle()
    {
        var vehicle = TestVehicle.Create();
        Assert.That(vehicle.AuditRecords.Single().VehicleId, Is.EqualTo(vehicle.Id));
    }

    [Test]
    public void AuditRecordIsCopyOfVehicle()
    {
        var vehicle = TestVehicle.Create();
        
        Assert.Multiple(() =>
        {
            var auditRecord = vehicle.AuditRecords.Single();
            Assert.That(auditRecord.Type, Is.EqualTo(vehicle.Type));
            Assert.That(auditRecord.FuelType, Is.EqualTo(vehicle.FuelType));
            Assert.That(auditRecord.FuelLevel, Is.EqualTo(vehicle.FuelLevel));
            Assert.That(auditRecord.TankCapacity, Is.EqualTo(vehicle.TankCapacity));
        });
    }
    
    [Test]
    public void NewVehicleIncludesOneInitialisationAuditRecord()
    {
        var vehicle = TestVehicle.Create();
        Assert.Multiple(() =>
        {
            Assert.That(vehicle.AuditRecords.Count(), Is.EqualTo(1));
            
            var auditRecord = vehicle.AuditRecords.Single();
            Assert.That(auditRecord.Metadata.Count(), Is.EqualTo(1));

            var auditRecordMetadata = auditRecord.Metadata.Single();
            Assert.That(auditRecordMetadata.PropertyName, Is.EqualTo("CreationDateTimeUtc"));
        });
    }
    
    private class TestVehicle : Vehicle
    {
        public TestVehicle(
            VehicleType type,
            FuelType fuelType,
            int fuelLevel,
            int tankCapacity) 
            : base(type, fuelType, fuelLevel, tankCapacity) { }

        public static TestVehicle Create() 
            => new(VehicleType.Car, FuelType.Diesel, 100, 150);
    }
}
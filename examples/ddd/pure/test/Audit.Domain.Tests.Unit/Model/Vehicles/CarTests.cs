using Audit.Domain.Tests.Unit.Model.Vehicles.Car;

namespace Audit.Domain.Tests.Unit.Model.Vehicles;

[TestFixture]
public class CarTests
{
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
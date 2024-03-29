using Audit.Domain.Abstraction.Enums;
using Audit.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Audit.Infrastructure.Tests.Integration.Persistence.EntityFramework.Repositories.ForecourtRepository;

using ForecourtRepository = Infrastructure.Persistence.EntityFramework.Write.Repositories.ForecourtRepository;

[TestFixture]
public class UpdateTests
{
    [TearDown]
    public async Task TearDown()
    {
        var dbContext = EfSqlLiteDatabaseSetUpFixture.DbContext;
        await dbContext.Forecourts.ExecuteDeleteAsync();
        await dbContext.SaveChangesAsync();
    }
    
    [Test]
    public async Task WhenPumpIsFilling_AndChangesAreSaved_ThenPumpVehicleIdIsUpdated()
    {
        // Arrange
        var forecourt = new Forecourt(Guid.NewGuid());
        var lane1 = forecourt.AddLane();
        lane1.AddPump();
        
        var lane2 = forecourt.AddLane();
        lane2.AddPump();
        var pump = lane2.AddPump();

        var car = new Vehicle(
            Guid.NewGuid(),
            VehicleType.Car,
            FuelType.Unleaded,
            10,
            100);
        
        var sut = new ForecourtRepository(EfSqlLiteDatabaseSetUpFixture.DbContext);
        await sut.AddAsync(forecourt, CancellationToken.None);
        await sut.SaveChangesAsync(CancellationToken.None);

        // Act
        pump.Filling(car);
        await sut.SaveChangesAsync(CancellationToken.None);
        
        // Assert
        var dbContext = EfSqlLiteDatabaseSetUpFixture.DbContext;
        var dbPump = await dbContext.Pumps.SingleAsync(x => x.Id == pump.Id);
        
        Assert.That(dbPump.VehicleId, Is.EqualTo(car.Id));
    }
    
    [Test]
    public async Task WhenPumpIsFilling_AndChangesAreSaved_ThenAPumpAuditRecordIsAddedToDatabase()
    {
        // Arrange
        var forecourt = new Forecourt(Guid.NewGuid());
        var lane1 = forecourt.AddLane();
        lane1.AddPump();
        
        var lane2 = forecourt.AddLane();
        lane2.AddPump();
        var pump = lane2.AddPump();

        var car = new Vehicle(
            Guid.NewGuid(),
            VehicleType.Car,
            FuelType.Unleaded,
            10,
            100);
        
        var sut = new ForecourtRepository(EfSqlLiteDatabaseSetUpFixture.DbContext);
        await sut.AddAsync(forecourt, CancellationToken.None);
        await sut.SaveChangesAsync(CancellationToken.None);
        EfSqlLiteDatabaseSetUpFixture.ResetDateTimeProvider();

        // Act
        pump.Filling(car);
        await sut.SaveChangesAsync(CancellationToken.None);
        
        // Assert
        var dbContext = EfSqlLiteDatabaseSetUpFixture.DbContext;
        var dbPumpAuditRecords = await dbContext.PumpAuditRecords.Where(x => x.PumpId == pump.Id).ToListAsync();
        var latestAuditRecord = dbPumpAuditRecords.OrderByDescending(x => x.Timestamp).First();
        
        Assert.Multiple(() =>
        {
            Assert.That(latestAuditRecord.PumpId, Is.EqualTo(pump.Id));
            Assert.That(latestAuditRecord.VehicleId, Is.EqualTo(car.Id));
        });
    }
    
    [Test]
    public async Task WhenPumpIsFilling_AndChangesAreSaved_ThenAPumpAuditMetadataRecordIsAddedToDatabase()
    {
        // Arrange
        var forecourt = new Forecourt(Guid.NewGuid());
        var lane1 = forecourt.AddLane();
        lane1.AddPump();
        
        var lane2 = forecourt.AddLane();
        lane2.AddPump();
        var pump = lane2.AddPump();

        var car = new Vehicle(
            Guid.NewGuid(),
            VehicleType.Car,
            FuelType.Unleaded,
            10,
            100);
        
        var sut = new ForecourtRepository(EfSqlLiteDatabaseSetUpFixture.DbContext);
        await sut.AddAsync(forecourt, CancellationToken.None);
        await sut.SaveChangesAsync(CancellationToken.None);
        EfSqlLiteDatabaseSetUpFixture.ResetDateTimeProvider();

        // Act
        pump.Filling(car);
        await sut.SaveChangesAsync(CancellationToken.None);
        
        // Assert
        var dbContext = EfSqlLiteDatabaseSetUpFixture.DbContext;
        var dbPumpAuditRecords = await dbContext.PumpAuditRecords
            .Include(x => x.Metadata)
            .Where(x => x.PumpId == pump.Id).ToListAsync();
        var latestAuditRecord = dbPumpAuditRecords.OrderByDescending(x => x.Timestamp).First();
        
        Assert.Multiple(() =>
        {
            Assert.That(latestAuditRecord.Metadata, Has.Count.EqualTo(1));
            Assert.That(latestAuditRecord.Metadata.Single().PropertyName, Is.EqualTo(nameof(Pump.VehicleId)));
            Assert.That(latestAuditRecord.Metadata.Single().OriginalValue, Is.EqualTo(null));
            Assert.That(latestAuditRecord.Metadata.Single().UpdatedValue, Is.EqualTo(car.Id.ToString()));
        });
    }
}
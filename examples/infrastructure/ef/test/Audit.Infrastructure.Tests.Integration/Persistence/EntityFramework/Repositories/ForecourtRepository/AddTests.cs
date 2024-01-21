using Audit.Domain.Abstraction.Enums;
using Audit.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Audit.Infrastructure.Tests.Integration.Persistence.EntityFramework.Repositories.ForecourtRepository;

using ForecourtRepository = Infrastructure.Persistence.EntityFramework.Repositories.ForecourtRepository;

[TestFixture]
public class AddTests
{
    private readonly DateTimeOffset _dateTimeUtcNow = DateTimeOffset.UtcNow;
    private Forecourt _forecourt = null!;
    
    [OneTimeSetUp]
    public async Task SetUp()
    {
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
        
        pump.Filling(car);
        
        var sut = new ForecourtRepository(EfSqlLiteDatabaseSetUpFixture.DbContext);
        await sut.AddAsync(forecourt, CancellationToken.None);
        await sut.SaveChangesAsync(CancellationToken.None);

        _forecourt = forecourt;
    }
    
    [OneTimeTearDown]
    public async Task TearDown()
    {
        var dbContext = EfSqlLiteDatabaseSetUpFixture.DbContext;
        await dbContext.Forecourts.ExecuteDeleteAsync();
        await dbContext.SaveChangesAsync();
    }
    
    [Test]
    public async Task ForecourtIsAddedToDatabase()
    {
        var dbContext = EfSqlLiteDatabaseSetUpFixture.DbContext;

        var dbForecourt = await dbContext.Forecourts
            .Where(x => x.Id == _forecourt.Id)
            .ToListAsync();
        
        Assert.Multiple(() =>
        {
            Assert.That(dbForecourt, Has.Count.EqualTo(1));
            Assert.That(dbForecourt.Single().Id, Is.EqualTo(_forecourt.Id));
        });
    }
    
    [Test]
    public async Task ForecourtLanesAreAddedToDatabase()
    {
        var dbContext = EfSqlLiteDatabaseSetUpFixture.DbContext;

        var dbForecourt = await dbContext.Forecourts
            .Include(x => x.Lanes)
            .SingleAsync(x => x.Id == _forecourt.Id);

        Assert.Multiple(() =>
        {
            foreach (var lane in _forecourt.Lanes)
            {
                var dbLane = dbForecourt.Lanes
                    .Where(x => x.Id == lane.Id)
                    .ToList();
                
                Assert.That(dbLane, Has.Count.EqualTo(1));
                Assert.That(dbLane.Single().Id, Is.EqualTo(lane.Id));
                Assert.That(dbLane.Single().ForecourtId, Is.EqualTo(lane.ForecourtId));
            }
        });
    }
    
    [Test]
    public async Task ForecourtLanePumpsAreAddedToDatabase()
    {
        var dbContext = EfSqlLiteDatabaseSetUpFixture.DbContext;

        var dbForecourt = await dbContext.Forecourts
            .Include(x => x.Lanes)
            .ThenInclude(x => x.Pumps)
            .SingleAsync(x => x.Id == _forecourt.Id);
        
        Assert.Multiple(() =>
        {
            foreach (var pump in _forecourt.Lanes.SelectMany(x => x.Pumps))
            {
                var dbPump = dbForecourt.Lanes
                    .SelectMany(x => x.Pumps)
                    .Where(x => x.Id == pump.Id)
                    .ToList();
                
                Assert.That(dbPump, Has.Count.EqualTo(1));
                Assert.That(dbPump.Single().Id, Is.EqualTo(pump.Id));
                Assert.That(dbPump.Single().LaneId, Is.EqualTo(pump.LaneId));
            }
        });
    }
    
    [Test]
    public async Task PumpVehicleIdIsSet()
    {
        var dbContext = EfSqlLiteDatabaseSetUpFixture.DbContext;

        var dbForecourt = await dbContext.Forecourts
            .Include(x => x.Lanes)
            .ThenInclude(x => x.Pumps)
            .SingleAsync(x => x.Id == _forecourt.Id);
        
        Assert.Multiple(() =>
        {
            foreach (var pump in _forecourt.Lanes.SelectMany(x => x.Pumps))
            {
                var dbPump = dbForecourt.Lanes
                    .SelectMany(x => x.Pumps)
                    .Single(x => x.Id == pump.Id);
                
                Assert.That(dbPump.VehicleId, Is.EqualTo(pump.VehicleId));
            }
        });
    }
    
    [Test]
    public async Task ForEachPumpAPumpAuditRecordIsAddedToDatabase()
    {
        var dbContext = EfSqlLiteDatabaseSetUpFixture.DbContext;

        var dbForecourt = await dbContext.Forecourts
            .Include(x => x.Lanes)
            .ThenInclude(x => x.Pumps)
            .ThenInclude(pump => pump.AuditRecords)
            .SingleAsync(x => x.Id == _forecourt.Id);
        
        Assert.Multiple(() =>
        {
            foreach (var pump in _forecourt.Lanes.SelectMany(x => x.Pumps))
            {
                var dbPump = dbForecourt.Lanes
                    .SelectMany(x => x.Pumps)
                    .Single(x => x.Id == pump.Id);
                
                Assert.That(dbPump.AuditRecords.ToList(), Has.Count.EqualTo(1));
                Assert.That(dbPump.AuditRecords.Single().PumpId, Is.EqualTo(pump.Id));
                Assert.That(dbPump.AuditRecords.Single().LaneId, Is.EqualTo(pump.LaneId));
                Assert.That(dbPump.AuditRecords.Single().VehicleId, Is.EqualTo(pump.VehicleId));
            }
        });
    }
    
    [Test]
    public async Task PumpAuditRecordTimestampIsSetToDateTimeUtcNow()
    {
        var dbContext = EfSqlLiteDatabaseSetUpFixture.DbContext;

        var dbForecourt = await dbContext.Forecourts
            .Include(x => x.Lanes)
            .ThenInclude(x => x.Pumps)
            .ThenInclude(pump => pump.AuditRecords)
            .SingleAsync(x => x.Id == _forecourt.Id);
        
        Assert.Multiple(() =>
        {
            foreach (var pump in _forecourt.Lanes.SelectMany(x => x.Pumps))
            {
                var dbPump = dbForecourt.Lanes
                    .SelectMany(x => x.Pumps)
                    .Single(x => x.Id == pump.Id);
                
                var dbPumpAuditRecord = dbPump.AuditRecords.Single();
                
                Assert.That(dbPumpAuditRecord.Timestamp, Is.EqualTo(_dateTimeUtcNow).Within(5).Seconds);
            }
        });
    }
    
    [Test]
    public async Task ForEachPumpAuditRecordACreationMetadataIsAddedToDatabase()
    {
        var dbContext = EfSqlLiteDatabaseSetUpFixture.DbContext;

        var dbForecourt = await dbContext.Forecourts
            .Include(x => x.Lanes)
            .ThenInclude(x => x.Pumps)
            .ThenInclude(pump => pump.AuditRecords)
            .SingleAsync(x => x.Id == _forecourt.Id);
        
        Assert.Multiple(() =>
        {
            var pumpAuditRecords = _forecourt.Lanes
                .SelectMany(x => x.Pumps)
                .SelectMany(x => x.AuditRecords);
            foreach (var pumpAuditRecord in pumpAuditRecords)
            {
                var dbPumpAuditRecord = dbForecourt.Lanes
                    .SelectMany(x => x.Pumps)
                    .SelectMany(x => x.AuditRecords)
                    .Single(x => x.Id == pumpAuditRecord.Id);
                
                Assert.That(dbPumpAuditRecord.Metadata.ToList(), Has.Count.EqualTo(1));
                Assert.That(dbPumpAuditRecord.Metadata.Single().AuditRecordId, Is.EqualTo(pumpAuditRecord.Id));
                Assert.That(dbPumpAuditRecord.Metadata.Single().PropertyName, Is.EqualTo("CreationDateTimeUtc"));
                Assert.That(dbPumpAuditRecord.Metadata.Single().OriginalValue, Is.Null);
                Assert.That(dbPumpAuditRecord.Metadata.Single().UpdatedValue, Is.EqualTo(_dateTimeUtcNow.ToString()).Within(5).Seconds);
            }
        });
    }
}
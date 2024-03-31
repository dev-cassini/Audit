using Audit.Application.Pumps.Queries.AuditRecords;
using Audit.Domain.Abstraction.Enums;
using Audit.Domain.Model;
using Audit.Infrastructure.Persistence.EntityFramework.Read.Queries.Pumps.AuditRecords;
using Microsoft.EntityFrameworkCore;

namespace Audit.Infrastructure.Tests.Integration.Persistence.EntityFramework.Read.Queries.Pumps.AuditRecords;

[TestFixture]
public class QueryHandlerTests
{
    [TearDown]
    public async Task TearDown()
    {
        var writeDbContext = EfSqlLiteDatabaseSetUpFixture.WriteDbContext;
        await writeDbContext.Forecourts.ExecuteDeleteAsync();
        await writeDbContext.SaveChangesAsync();
    }
    
    [Test]
    public async Task WhenForecourtDoesNotExist_ThenEmptyListIsReturned()
    {
        // Arrange
        var query = new Query(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
        var sut = new QueryHandler(EfSqlLiteDatabaseSetUpFixture.ReadDbContext);

        // Act
        var actual = await sut.Handle(query, CancellationToken.None);
        
        // Assert
        Assert.That(actual, Is.Empty);
    }
    
    [Test]
    public async Task WhenLaneDoesNotExist_ThenEmptyListIsReturned()
    {
        // Arrange
        var writeDbContext = EfSqlLiteDatabaseSetUpFixture.WriteDbContext;
        var forecourt = new Forecourt(Guid.NewGuid());
        await writeDbContext.Forecourts.AddAsync(forecourt);
        await writeDbContext.SaveChangesAsync();
        
        var query = new Query(forecourt.Id, Guid.NewGuid(), Guid.NewGuid());
        var sut = new QueryHandler(EfSqlLiteDatabaseSetUpFixture.ReadDbContext);

        // Act
        var actual = await sut.Handle(query, CancellationToken.None);
        
        // Assert
        Assert.That(actual, Is.Empty);
    }
    
    [Test]
    public async Task WhenPumpDoesNotExist_ThenEmptyListIsReturned()
    {
        // Arrange
        var writeDbContext = EfSqlLiteDatabaseSetUpFixture.WriteDbContext;
        var forecourt = new Forecourt(Guid.NewGuid());
        var lane = forecourt.AddLane();
        await writeDbContext.Forecourts.AddAsync(forecourt);
        await writeDbContext.SaveChangesAsync();
        
        var query = new Query(forecourt.Id, lane.Id, Guid.NewGuid());
        var sut = new QueryHandler(EfSqlLiteDatabaseSetUpFixture.ReadDbContext);

        // Act
        var actual = await sut.Handle(query, CancellationToken.None);
        
        // Assert
        Assert.That(actual, Is.Empty);
    }
    
    [Test]
    public async Task WhenPumpDoesExist_ButIsPartOfADifferentForecourt_ThenEmptyListIsReturned()
    {
        // Arrange
        var writeDbContext = EfSqlLiteDatabaseSetUpFixture.WriteDbContext;
        var forecourt = new Forecourt(Guid.NewGuid());
        var lane = forecourt.AddLane();
        var pump = lane.AddPump();
        await writeDbContext.Forecourts.AddAsync(forecourt);
        await writeDbContext.SaveChangesAsync();
        
        var query = new Query(Guid.NewGuid(), Guid.NewGuid(), pump.Id);
        var sut = new QueryHandler(EfSqlLiteDatabaseSetUpFixture.ReadDbContext);

        // Act
        var actual = await sut.Handle(query, CancellationToken.None);
        
        // Assert
        Assert.That(actual, Is.Empty);
    }
    
    [Test]
    public async Task WhenPumpDoesExist_ThenAuditRecordsAreReturned()
    {
        // Arrange
        var writeDbContext = EfSqlLiteDatabaseSetUpFixture.WriteDbContext;
        var forecourt = new Forecourt(Guid.NewGuid());
        var lane = forecourt.AddLane();
        var pump = lane.AddPump();
        await writeDbContext.Forecourts.AddAsync(forecourt);
        await writeDbContext.SaveChangesAsync();
        
        var car = new Vehicle(
            Guid.NewGuid(),
            VehicleType.Car,
            FuelType.Unleaded,
            10,
            100);
        
        pump.Filling(car);
        await writeDbContext.SaveChangesAsync();

        var auditRecords = await writeDbContext.PumpAuditRecords
            .Include(x => x.Metadata)
            .Where(x => x.PumpId == pump.Id)
            .ToListAsync();
        
        var query = new Query(forecourt.Id, lane.Id, pump.Id);
        var sut = new QueryHandler(EfSqlLiteDatabaseSetUpFixture.ReadDbContext);

        // Act
        var actual = (await sut.Handle(query, CancellationToken.None)).ToList();
        
        // Assert
        Assert.That(actual, Has.Count.EqualTo(auditRecords.Count));
        foreach (var auditRecordDto in actual)
        {
            var auditRecord = auditRecords.Single(x => x.Id == auditRecordDto.Id);
            Assert.That(auditRecordDto.Metadata.ToList(), Has.Count.EqualTo(auditRecord.Metadata.Count));
        }
    }
}
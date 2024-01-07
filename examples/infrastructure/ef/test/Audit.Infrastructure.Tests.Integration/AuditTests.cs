using Audit.Core;
using Audit.Domain.Model;
using Audit.Domain.Model.Enums;
using Microsoft.EntityFrameworkCore;

namespace Audit.Infrastructure.Tests.Integration;

[TestFixture]
public class AuditTests
{
    [TearDown]
    public async Task TearDown()
    {
        await using var dbContext = DbContextFactory.Create();
        await dbContext.Pumps.ExecuteDeleteAsync();
        await dbContext.Vehicles.ExecuteDeleteAsync();
    }
    
    [Test]
    public async Task Test()
    {
        Configuration
            .Setup()
            .UseEntityFramework(x => x
                .UseDbContext<DbContext>()
                .AuditTypeNameMapper(typeName => $"{typeName}Audit"));
        
        var car = new Vehicle(
            Guid.NewGuid(),
            VehicleType.Car,
            FuelType.Diesel,
            100,
            100);

        var forecourt = new Forecourt(Guid.NewGuid());
        var lane = new Lane(Guid.NewGuid(), forecourt);
        var pump = new Pump(Guid.NewGuid(), lane);

        await using var dbContext = DbContextFactory.Create();
        await dbContext.Vehicles.AddAsync(car);
        await dbContext.Pumps.AddAsync(pump);
        await dbContext.SaveChangesAsync();
    }
}
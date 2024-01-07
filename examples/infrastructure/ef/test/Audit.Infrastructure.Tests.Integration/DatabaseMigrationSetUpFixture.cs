using Microsoft.EntityFrameworkCore;

namespace Audit.Infrastructure.Tests.Integration;

[SetUpFixture]
public class DatabaseMigrationSetUpFixture
{
    [OneTimeSetUp]
    public async Task MigrateDatabaseAsync()
    {
        await using var dbContext = DbContextFactory.Create();
        await dbContext.Database.MigrateAsync();
    }
}
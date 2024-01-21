using Audit.Domain.Abstraction.Tooling;
using Audit.Domain.Tooling;
using Microsoft.Data.Sqlite;

namespace Audit.Infrastructure.Tests.Integration.Persistence.EntityFramework;

[SetUpFixture]
public static class EfSqlLiteDatabaseSetUpFixture
{
    private static SqliteConnection SqliteConnection { get; set; } = null!;

    public static IDateTimeProvider DateTimeProvider { get; private set; } = null!;
    public static TestDbContext DbContext { get; private set; } = null!;
    
    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        var dateTimeProvider = new DateTimeProvider();
        var sqliteConnection = new SqliteConnection("DataSource=:memory:");
        sqliteConnection.Open();
        
        var dbContext = new TestDbContext(sqliteConnection, dateTimeProvider);
        dbContext.Database.EnsureCreated();
        
        SqliteConnection = sqliteConnection;
        DateTimeProvider = dateTimeProvider;
        DbContext = dbContext;
    }

    [OneTimeTearDown]
    public static void OneTimeTearDown()
    {
        SqliteConnection.Close();
    }
}
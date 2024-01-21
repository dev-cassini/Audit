using Microsoft.Data.Sqlite;

namespace Audit.Infrastructure.Tests.Integration.Persistence.EntityFramework;

[SetUpFixture]
public static class EfSqlLiteDatabaseSetUpFixture
{
    private static SqliteConnection SqliteConnection { get; set; } = null!;
    public static TestDbContext DbContext { get; private set; } = null!;
    
    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        var sqliteConnection = new SqliteConnection("DataSource=:memory:");
        sqliteConnection.Open();
        
        var dbContext = new TestDbContext(sqliteConnection);
        dbContext.Database.EnsureCreated();
        
        SqliteConnection = sqliteConnection;
        DbContext = dbContext;
    }

    [OneTimeTearDown]
    public static void OneTimeTearDown()
    {
        SqliteConnection.Close();
    }
}
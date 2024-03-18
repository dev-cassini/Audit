using Audit.Domain.Abstraction.Tooling;
using Microsoft.Data.Sqlite;
using Moq;

namespace Audit.Infrastructure.Tests.Integration.Persistence.EntityFramework;

[SetUpFixture]
public static class EfSqlLiteDatabaseSetUpFixture
{
    private static SqliteConnection SqliteConnection { get; set; } = null!;
    private static readonly Mock<IDateTimeProvider> DateTimeProviderMock = new();

    public static IDateTimeProvider DateTimeProvider => DateTimeProviderMock.Object;
    public static TestDbContext DbContext { get; private set; } = null!;
    
    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        DateTimeProviderMock.Setup(x => x.UtcNow).Returns(DateTimeOffset.UtcNow);
        var sqliteConnection = new SqliteConnection("DataSource=:memory:");
        sqliteConnection.Open();
        
        var dbContext = new TestDbContext(sqliteConnection, DateTimeProvider);
        dbContext.Database.EnsureCreated();
        
        SqliteConnection = sqliteConnection;
        DbContext = dbContext;
    }

    [OneTimeTearDown]
    public static void OneTimeTearDown()
    {
        SqliteConnection.Close();
    }

    public static void ResetDateTimeProvider()
    {
        DateTimeProviderMock.Reset();
        DateTimeProviderMock.Setup(x => x.UtcNow).Returns(DateTimeOffset.UtcNow);
    }
}
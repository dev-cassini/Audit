using Audit.Domain.Abstraction.Tooling;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Audit.Infrastructure.Tests.Integration.Persistence.EntityFramework;

using EntityFramework = Audit.Infrastructure.Persistence.EntityFramework;

[SetUpFixture]
public static class EfSqlLiteDatabaseSetUpFixture
{
    private static SqliteConnection? SqliteConnection { get; set; }
    private static readonly Mock<IDateTimeProvider> DateTimeProviderMock = new();

    public static IDateTimeProvider DateTimeProvider => DateTimeProviderMock.Object;
    public static EntityFramework.Read.DbContext ReadDbContext { get; private set; } = null!;
    public static EntityFramework.Write.DbContext WriteDbContext { get; private set; } = null!;
    
    [OneTimeSetUp]
    public static void OneTimeSetUp()
    {
        DateTimeProviderMock.Setup(x => x.UtcNow).Returns(DateTimeOffset.UtcNow);
        var sqliteConnection = new SqliteConnection("DataSource=:memory:");
        sqliteConnection.Open();
        
        var configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
        var serviceProvider = new ServiceCollection()
            .AddSingleton(DateTimeProvider)
            .AddSingleton(sqliteConnection)
            .AddInfrastructure(infrastructureConfigurator =>
            {
                infrastructureConfigurator
                    .AddPersistence(persistenceConfigurator =>
                    {
                        persistenceConfigurator.AddEntityFramework(efConfigurator =>
                        {
                            efConfigurator.AddDbContext<EntityFramework.Read.DbContext>(
                                configuration,
                                dbConfigurator =>
                                {
                                    dbConfigurator
                                        .UseSqlite(sqliteConnection)
                                        .EnableSensitiveDataLogging();
                                },
                                null,
                                ServiceLifetime.Singleton,
                                ServiceLifetime.Singleton);
                            
                            efConfigurator.AddDbContext<EntityFramework.Write.DbContext>(
                                configuration,
                                dbConfigurator =>
                                {
                                    dbConfigurator
                                        .UseSqlite(sqliteConnection)
                                        .EnableSensitiveDataLogging();
                                },
                                interceptorConfigurator => interceptorConfigurator.AddAuditInterceptors(typeof(Domain.AssemblyMarker).Assembly),
                                ServiceLifetime.Singleton,
                                ServiceLifetime.Singleton);
                        });
                    });
            }).BuildServiceProvider();
        
        var dbContext = serviceProvider.GetRequiredService<EntityFramework.Write.DbContext>();
        dbContext.Database.EnsureCreated();
        
        SqliteConnection = sqliteConnection;
        ReadDbContext = serviceProvider.GetRequiredService<EntityFramework.Read.DbContext>();
        WriteDbContext = dbContext;
    }

    [OneTimeTearDown]
    public static void OneTimeTearDown()
    {
        SqliteConnection?.Close();
    }

    public static void ResetDateTimeProvider()
    {
        DateTimeProviderMock.Reset();
        DateTimeProviderMock.Setup(x => x.UtcNow).Returns(DateTimeOffset.UtcNow);
    }
}
using Audit.Infrastructure.Persistence.EntityFramework;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Testcontainers.PostgreSql;

namespace Audit.ComponentTests;

[SetUpFixture]
public class WebApplicationSetUpFixture
{
    private INetwork _network;
    private IContainer _postgresContainer;
    private static WebApplicationFactory<Program> _factory = null!;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _network = new NetworkBuilder().WithName("audit-component-tests").Build();
        await _network.CreateAsync();
        
        _postgresContainer = new PostgreSqlBuilder()
            .WithNetwork(_network)
            .WithImage("postgres:latest")
            .WithPortBinding(9903, 5432)
            .WithUsername("postgres")
            .WithPassword("password")
            .WithWaitStrategy(
                Wait.ForUnixContainer()
                    .UntilMessageIsLogged(
                        "database system is ready to accept connections", 
                        x => x.WithTimeout(TimeSpan.FromSeconds(30))))
            .Build();
        
        await _postgresContainer.StartAsync();
        
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                var configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(new List<KeyValuePair<string, string?>>
                    {
                        new("ConnectionStrings:Postgres", "Host=localhost:9903;Username=postgres;Password=password;Database=Audit.Ef.V2.ComponentTests;Include Error Detail=true"),
                    })
                    .Build();

                builder
                    .UseEnvironment("Test")
                    .UseConfiguration(configuration)
                    .ConfigureTestServices(collection =>
                    {
                    });
            });
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _factory.DisposeAsync();
        await _postgresContainer.StopAsync();
        await _postgresContainer.DisposeAsync();
        await _network.DeleteAsync();
        await _network.DisposeAsync();
    }

    public static HttpClient CreateHttpClient()
    {
        return _factory.CreateClient();
    }

    public static AuditDbContext CreateDbContext()
    {
        return _factory.Services.CreateScope().ServiceProvider.GetRequiredService<AuditDbContext>();
    }
}
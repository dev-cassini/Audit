using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Persistence.EntityFramework.Databases.Postgres;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgresDatabase(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<DbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Postgres"));
        });

        return serviceCollection;
    }
}
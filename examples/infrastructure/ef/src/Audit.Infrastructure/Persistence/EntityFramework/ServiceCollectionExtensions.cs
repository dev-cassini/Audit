using Audit.Infrastructure.Persistence.EntityFramework.Databases.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Persistence.EntityFramework;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEntityFramework(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddPostgresDatabase(configuration);
        return serviceCollection;
    }
}
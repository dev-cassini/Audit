using Audit.Infrastructure.Persistence;
using Audit.Infrastructure.Persistence.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection serviceCollection, 
        IConfiguration configuration)
    {
        serviceCollection.AddPersistence(configuration);
        return serviceCollection;
    }

    public static IServiceProvider UseInfrastructure(this IServiceProvider serviceProvider)
    {
        serviceProvider.MigrateDatabase();
        return serviceProvider;
    }
}
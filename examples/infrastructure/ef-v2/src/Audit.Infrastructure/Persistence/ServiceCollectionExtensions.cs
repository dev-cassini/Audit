using Audit.Infrastructure.Persistence.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Persistence;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddPersistence(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddEntityFramework(configuration);
        return serviceCollection;
    }
}
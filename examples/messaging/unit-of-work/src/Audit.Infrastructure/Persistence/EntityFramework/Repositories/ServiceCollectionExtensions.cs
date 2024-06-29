using Audit.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Persistence.EntityFramework.Repositories;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddEfRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IForecourtRepository, EfForecourtRepository>();

        return serviceCollection;
    }
}
using Audit.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Persistence.EntityFramework.Repositories;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEfRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IForecourtRepository, EfForecourtRepository>();

        return serviceCollection;
    }
}
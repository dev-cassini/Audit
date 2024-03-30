using Audit.Domain.Abstraction.Tooling;
using Audit.Domain.Tooling;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IDateTimeProvider, DateTimeProvider>();

        return serviceCollection;
    }
}
using Audit.Domain.Tooling;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomainTooling(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDateTimeProvider, DefaultDateTimeProvider>();
        return serviceCollection;
    }
}
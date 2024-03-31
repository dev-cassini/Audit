using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Messaging.MediatR;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddMediatR(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(configuration =>
        {
            configuration
                .RegisterServicesFromAssemblyContaining<AssemblyMarker>();
        });
        
        return serviceCollection;
    }
}
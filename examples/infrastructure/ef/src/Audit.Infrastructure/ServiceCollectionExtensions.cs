using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection serviceCollection, 
        Action<Configurator> configuratorAction)
    {
        var configurator = new Configurator(serviceCollection);
        configuratorAction.Invoke(configurator);
        
        return serviceCollection;
    }
}
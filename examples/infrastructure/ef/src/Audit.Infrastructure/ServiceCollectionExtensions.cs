using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection serviceCollection, 
        IConfiguration configuration,
        Action<Configurator> configuratorAction)
    {
        var configurator = new Configurator(serviceCollection, configuration);
        configuratorAction.Invoke(configurator);
        
        return serviceCollection;
    }
}
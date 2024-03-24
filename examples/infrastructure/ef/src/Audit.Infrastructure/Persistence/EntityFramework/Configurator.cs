using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Persistence.EntityFramework;

public class Configurator
{
    private readonly IServiceCollection _serviceCollection;

    public Configurator(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public Configurator AddDbContext<T>(
        IConfiguration configuration,
        Action<Databases.Configurator> dbConfiguratorAction,
        Action<Interceptors.Configurator>? interceptorConfiguratorAction = null,
        ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
        ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
        where T : Microsoft.EntityFrameworkCore.DbContext
    {
        _serviceCollection.AddDbContext<T>((serviceProvider, builder) =>
            {
                var dbConfigurator = new Databases.Configurator(builder, configuration);
                dbConfiguratorAction.Invoke(dbConfigurator);

                var interceptorConfigurator = new Interceptors.Configurator(serviceProvider, builder);
                interceptorConfiguratorAction?.Invoke(interceptorConfigurator);
            },
            contextLifetime,
            optionsLifetime);
        
        return this;
    }
}
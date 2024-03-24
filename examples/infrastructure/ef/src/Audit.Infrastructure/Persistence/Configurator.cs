using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Persistence;

public class Configurator
{
    private readonly IServiceCollection _serviceCollection;

    public Configurator(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }
    
    public Configurator AddEntityFramework(Action<EntityFramework.Configurator> configuratorAction)
    {
        var configurator = new EntityFramework.Configurator(_serviceCollection);
        configuratorAction.Invoke(configurator);
        
        return this;
    }
}
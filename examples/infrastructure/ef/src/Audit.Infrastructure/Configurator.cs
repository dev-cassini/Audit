using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure;

public class Configurator
{
    private readonly IServiceCollection _serviceCollection;

    public Configurator(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }
    
    public Configurator AddPersistence(Action<Persistence.Configurator> configuratorAction)
    {
        var configurator = new Persistence.Configurator(_serviceCollection);
        configuratorAction.Invoke(configurator);
        
        return this;
    }
}
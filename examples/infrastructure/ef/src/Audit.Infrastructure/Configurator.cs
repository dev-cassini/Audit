using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure;

public class Configurator(IServiceCollection serviceCollection)
{
    public Configurator AddMessaging(Action<Messaging.Configurator> configuratorAction)
    {
        var configurator = new Messaging.Configurator(serviceCollection);
        configuratorAction.Invoke(configurator);
        
        return this;
    }
    
    public Configurator AddPersistence(Action<Persistence.Configurator> configuratorAction)
    {
        var configurator = new Persistence.Configurator(serviceCollection);
        configuratorAction.Invoke(configurator);
        
        return this;
    }
}
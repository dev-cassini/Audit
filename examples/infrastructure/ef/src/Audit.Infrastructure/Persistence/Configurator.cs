using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Persistence;

public class Configurator(IServiceCollection serviceCollection)
{
    public Configurator AddEntityFramework(Action<EntityFramework.Configurator> configuratorAction)
    {
        var configurator = new EntityFramework.Configurator(serviceCollection);
        configuratorAction.Invoke(configurator);
        
        return this;
    }
}
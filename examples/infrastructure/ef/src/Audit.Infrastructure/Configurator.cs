using Audit.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure;

public class Configurator
{
    private readonly IServiceCollection _serviceCollection;
    private readonly IConfiguration _configuration;

    public Configurator(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        _serviceCollection = serviceCollection;
        _configuration = configuration;
    }
    
    public Configurator AddPersistence(Action<Persistence.Configurator> configuratorAction)
    {
        _serviceCollection.AddPersistence(_configuration, configuratorAction);
        return this;
    }
}
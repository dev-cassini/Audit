using Audit.Infrastructure.Messaging.MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Messaging;

public class Configurator(IServiceCollection serviceCollection)
{
    public Configurator AddMediatR()
    {
        serviceCollection.AddMediatR();
        return this;
    }
}
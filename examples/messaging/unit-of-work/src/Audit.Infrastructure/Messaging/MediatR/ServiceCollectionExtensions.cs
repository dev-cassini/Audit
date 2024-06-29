using System.Reflection;
using Audit.Infrastructure.Messaging.MediatR.PipelineBehaviours;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Messaging.MediatR;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddMediatR(
        this IServiceCollection serviceCollection,
        Assembly applicationAssembly)
    {
        serviceCollection.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(applicationAssembly);
            configuration.AddOpenBehavior(typeof(UnitOfWorkPipelineBehaviour<,>));
        });
        
        return serviceCollection;
    }
}
using Audit.Domain.Tooling;
using Audit.Infrastructure.Persistence.EntityFramework.Interceptors.SaveChanges;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Persistence.EntityFramework;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddEntityFramework(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<AuditDbContext>((provider, builder) =>
        {
            var connectionString = configuration.GetConnectionString("Postgres");
            var dateTimeProvider = provider.GetRequiredService<IDateTimeProvider>();
            builder
                .UseNpgsql(connectionString)
                .AddInterceptors(
                    new CreateInitialAuditInterceptor(), 
                    new CreateUpdateAuditInterceptor(dateTimeProvider));
        });
        
        return serviceCollection;
    }
}
using Audit.Core;
using Audit.Infrastructure.Persistence.EntityFramework.Databases.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Persistence.EntityFramework;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEntityFramework(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddPostgresDatabase(configuration);

        Configuration
            .Setup()
            .UseEntityFramework(x => x
                .UseDbContext<DbContext>()
                .AuditTypeNameMapper(typeName => $"{typeName}Audit"));
        
        return serviceCollection;
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Audit.Infrastructure.Persistence.EntityFramework;

public static class ServiceProviderExtensions
{
    public static IServiceProvider MigrateDatabase(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<DbContext>();
        dbContext.Database.Migrate();

        return serviceProvider;
    }
}
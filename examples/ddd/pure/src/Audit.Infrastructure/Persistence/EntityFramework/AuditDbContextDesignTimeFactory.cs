using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Audit.Infrastructure.Persistence.EntityFramework;

public class AuditDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AuditDbContext>
{
    public AuditDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AuditDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=password;Database=Audit.DDD.Pure;Include Error Detail=true");

        return new AuditDbContext(optionsBuilder.Options);
    }
}
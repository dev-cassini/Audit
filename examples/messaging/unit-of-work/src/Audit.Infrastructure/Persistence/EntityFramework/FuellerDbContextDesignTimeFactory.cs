using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Audit.Infrastructure.Persistence.EntityFramework;

public class FuellerDbContextDesignTimeFactory : IDesignTimeDbContextFactory<FuellerDbContext>
{
    public FuellerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FuellerDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=password;Database=Audit.Messaging.UnitOfWork;Include Error Detail=true");

        return new FuellerDbContext(optionsBuilder.Options);
    }
}
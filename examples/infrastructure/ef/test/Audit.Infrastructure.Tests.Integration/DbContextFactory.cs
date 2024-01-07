using Microsoft.EntityFrameworkCore;
using DbContext = Audit.Infrastructure.Persistence.EntityFramework.DbContext;

namespace Audit.Infrastructure.Tests.Integration;

public static class DbContextFactory
{
    private const string ConnectionString = "Host=localhost;Username=postgres;Password=Password123!;Database=Audit.Infrastructure.Ef.Tests;Include Error Detail=true";

    public static DbContext Create()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DbContext>()
            .EnableSensitiveDataLogging()
            .UseNpgsql(
                ConnectionString,
                builder => builder.EnableRetryOnFailure());

        return new DbContext(optionsBuilder.Options);
    }
}
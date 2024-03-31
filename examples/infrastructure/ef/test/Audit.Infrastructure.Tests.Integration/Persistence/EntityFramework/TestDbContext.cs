using Audit.Domain.Abstraction.Tooling;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Audit.Infrastructure.Tests.Integration.Persistence.EntityFramework;

using DbContext = Infrastructure.Persistence.EntityFramework.Write.DbContext;

public class TestDbContext(
    SqliteConnection sqliteConnection,
    IDateTimeProvider dateTimeProvider) : DbContext(dateTimeProvider)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .EnableSensitiveDataLogging()
            .UseSqlite(sqliteConnection);

        base.OnConfiguring(optionsBuilder);
    }
}
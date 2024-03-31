using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Audit.Infrastructure.Tests.Integration.Persistence.EntityFramework.Read;

using DbContext = Infrastructure.Persistence.EntityFramework.Read.DbContext;

public class TestDbContext(SqliteConnection sqliteConnection) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .EnableSensitiveDataLogging()
            .UseSqlite(sqliteConnection);

        base.OnConfiguring(optionsBuilder);
    }
}
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Audit.Infrastructure.Tests.Integration.Persistence.EntityFramework;

using DbContext = Infrastructure.Persistence.EntityFramework.DbContext;

public class TestDbContext : DbContext
{
    private readonly SqliteConnection _sqliteConnection;

    public TestDbContext(SqliteConnection sqliteConnection)
    {
        _sqliteConnection = sqliteConnection;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .EnableSensitiveDataLogging()
            .UseSqlite(_sqliteConnection);

        base.OnConfiguring(optionsBuilder);
    }
}
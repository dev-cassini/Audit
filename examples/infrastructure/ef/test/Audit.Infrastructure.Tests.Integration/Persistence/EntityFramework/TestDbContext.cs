using Audit.Domain.Abstraction.Tooling;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Audit.Infrastructure.Tests.Integration.Persistence.EntityFramework;

using DbContext = Infrastructure.Persistence.EntityFramework.Write.DbContext;

public class TestDbContext : DbContext
{
    private readonly SqliteConnection _sqliteConnection;

    public TestDbContext(
        SqliteConnection sqliteConnection,
        IDateTimeProvider dateTimeProvider) : base(dateTimeProvider)
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
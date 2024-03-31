using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Audit.Infrastructure.Persistence.EntityFramework.Databases;

public class Configurator(
    DbContextOptionsBuilder dbContextOptionsBuilder,
    IConfiguration configuration)
{
    public Configurator UsePostgres()
    {
        dbContextOptionsBuilder.UseNpgsql(configuration.GetConnectionString("Postgres"));
        return this;
    }
    
    public Configurator UseSqlite(SqliteConnection sqliteConnection)
    {
        dbContextOptionsBuilder.UseSqlite(sqliteConnection);
        return this;
    }
}
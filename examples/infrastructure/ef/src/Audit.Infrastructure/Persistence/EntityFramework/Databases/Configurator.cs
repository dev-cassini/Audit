using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Audit.Infrastructure.Persistence.EntityFramework.Databases;

public class Configurator
{
    private readonly DbContextOptionsBuilder _dbContextOptionsBuilder;
    private readonly IConfiguration _configuration;

    public Configurator(
        DbContextOptionsBuilder dbContextOptionsBuilder,
        IConfiguration configuration)
    {
        _dbContextOptionsBuilder = dbContextOptionsBuilder;
        _configuration = configuration;
    }

    public Configurator UsePostgres()
    {
        _dbContextOptionsBuilder.UseNpgsql(_configuration.GetConnectionString("Postgres"));
        return this;
    }
    
    public Configurator UseSqlite(SqliteConnection sqliteConnection)
    {
        _dbContextOptionsBuilder.UseSqlite(sqliteConnection);
        return this;
    }
}
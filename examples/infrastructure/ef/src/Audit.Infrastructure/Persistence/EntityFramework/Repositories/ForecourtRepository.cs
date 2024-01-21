using Audit.Domain.Model;

namespace Audit.Infrastructure.Persistence.EntityFramework.Repositories;

public class ForecourtRepository
{
    private readonly DbContext _dbContext;

    public ForecourtRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Forecourt forecourt, CancellationToken cancellationToken)
    {
        await _dbContext.Forecourts.AddAsync(forecourt, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
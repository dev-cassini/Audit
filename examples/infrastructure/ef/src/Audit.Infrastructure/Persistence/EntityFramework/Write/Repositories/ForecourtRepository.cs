using Audit.Domain.Model;

namespace Audit.Infrastructure.Persistence.EntityFramework.Write.Repositories;

public class ForecourtRepository(DbContext dbContext)
{
    public async Task AddAsync(Forecourt forecourt, CancellationToken cancellationToken)
    {
        await dbContext.Forecourts.AddAsync(forecourt, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
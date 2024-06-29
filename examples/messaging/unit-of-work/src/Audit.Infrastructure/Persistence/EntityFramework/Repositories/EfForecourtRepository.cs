using Audit.Domain.Model;
using Audit.Domain.Repositories;

namespace Audit.Infrastructure.Persistence.EntityFramework.Repositories;

public class EfForecourtRepository(FuellerDbContext dbContext) : IForecourtRepository
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
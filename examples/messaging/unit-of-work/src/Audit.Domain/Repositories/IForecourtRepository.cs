using Audit.Domain.Model;

namespace Audit.Domain.Repositories;

public interface IForecourtRepository
{
    Task AddAsync(Forecourt forecourt, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
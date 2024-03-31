using Audit.Domain.Abstraction.Model.Audit;
using Audit.Domain.Abstraction.Tooling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Audit.Infrastructure.Persistence.EntityFramework.Interceptors.SaveChanges;

public class CreateInitialAuditInterceptor<T>(IDateTimeProvider dateTimeProvider) : ISaveChangesInterceptor
    where T : IAuditRecord
{
    public ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null) return ValueTask.FromResult(result);
        
        var auditableEntries = eventData.Context.ChangeTracker
            .Entries()
            .Where(x => x.State is EntityState.Added)
            .Where(x => x.Entity is IAuditable<T>);
        
        foreach (var auditableEntry in auditableEntries)
        {
            var changes = new Dictionary<string, (string? OriginalValue, string? UpdatedValue)>
            {
                { "CreationDateTimeUtc", (null, dateTimeProvider.UtcNow.ToString()) }
            };
            
            var auditableEntity = (auditableEntry.Entity as IAuditable<T>)!;
            auditableEntity.AddAuditRecord(changes, dateTimeProvider);
        }

        return ValueTask.FromResult(result);
    }
}
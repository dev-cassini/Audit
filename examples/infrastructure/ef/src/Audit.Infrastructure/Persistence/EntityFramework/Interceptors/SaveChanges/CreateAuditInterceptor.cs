using Audit.Domain.Abstraction.Model.Audit;
using Audit.Domain.Abstraction.Tooling;
using Audit.Domain.Tooling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Audit.Infrastructure.Persistence.EntityFramework.Interceptors.SaveChanges;

public class CreateAuditInterceptor<T> : ISaveChangesInterceptor where T : IAuditRecord
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateAuditInterceptor(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    
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
            var auditableEntity = (auditableEntry.Entity as IAuditable<T>)!;

            var changes = new Dictionary<string, (string? OriginalValue, string? UpdatedValue)>
            {
                { "CreationDateTimeUtc", (null, _dateTimeProvider.UtcNow.ToString()) }
            };
            
            auditableEntity.AddAuditRecord(changes, _dateTimeProvider);
        }

        return ValueTask.FromResult(result);
    }
}
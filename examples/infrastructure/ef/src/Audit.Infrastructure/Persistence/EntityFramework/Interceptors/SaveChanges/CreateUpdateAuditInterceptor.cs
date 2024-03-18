using Audit.Domain.Abstraction.Model.Audit;
using Audit.Domain.Abstraction.Tooling;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Audit.Infrastructure.Persistence.EntityFramework.Interceptors.SaveChanges;

public class CreateUpdateAuditInterceptor<T> : ISaveChangesInterceptor where T : IAuditRecord
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateUpdateAuditInterceptor(IDateTimeProvider dateTimeProvider)
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
            .Where(x => x.State is EntityState.Modified)
            .Where(x => x.Entity is IAuditable<T>);
        
        foreach (var auditableEntry in auditableEntries)
        {
            var changes = new Dictionary<string, (string? OriginalValue, string? UpdatedValue)>();

            var modifiedProperties = auditableEntry.Properties.Where(x => x.IsModified);
            foreach (var modifiedProperty in modifiedProperties)
            {
                changes.Add(modifiedProperty.Metadata.Name, (modifiedProperty.OriginalValue?.ToString(), modifiedProperty.CurrentValue?.ToString()));
            }
         
            var auditableEntity = (auditableEntry.Entity as IAuditable<T>)!;
            auditableEntity.AddAuditRecord(changes, _dateTimeProvider);
        }

        return ValueTask.FromResult(result);
    }
}
using Audit.Domain;
using Audit.Domain.Model;
using Audit.Domain.Tooling;
using Audit.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Audit.Infrastructure.Persistence.EntityFramework.Interceptors.SaveChanges;

public class CreateUpdateAuditInterceptor(IDateTimeProvider dateTimeProvider) : ISaveChangesInterceptor
{
    public async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null) return await ValueTask.FromResult(result);
        
        var auditableEntries = eventData.Context.ChangeTracker
            .Entries()
            .Where(x => x.State is EntityState.Modified)
            .Where(x => x.Entity is IAuditable);
        
        foreach (var auditableEntry in auditableEntries)
        {
            var properties = auditableEntry.Properties
                .Where(x => x.IsModified)
                .ToDictionary(
                    property => property.Metadata.Name,
                    property => (property.OriginalValue?.ToString(), property.CurrentValue?.ToString()));
         
            var auditableEntity = (auditableEntry.Entity as IAuditable)!;
            var auditRecord = new AuditRecord(
                Guid.NewGuid(),
                auditableEntity.Id,
                dateTimeProvider.UtcNow,
                new Actor(Guid.NewGuid(), ""),
                properties);

            await eventData.Context.AddAsync(auditRecord, cancellationToken);
        }

        return await ValueTask.FromResult(result);
    }
}
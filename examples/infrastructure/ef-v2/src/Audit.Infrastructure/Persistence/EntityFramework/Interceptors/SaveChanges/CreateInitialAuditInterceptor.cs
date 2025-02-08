using Audit.Domain;
using Audit.Domain.Model;
using Audit.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Audit.Infrastructure.Persistence.EntityFramework.Interceptors.SaveChanges;

public class CreateInitialAuditInterceptor : ISaveChangesInterceptor
{
    public async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null) return await ValueTask.FromResult(result);
        
        var auditableEntries = eventData.Context.ChangeTracker
            .Entries()
            .Where(x => x.State is EntityState.Added)
            .Where(x => x.Entity is IAuditable);

        var auditRecords = new List<AuditRecord>();
        foreach (var auditableEntry in auditableEntries)
        {
            var excludedMetadataProperties = new List<string>
            {
                nameof(IAuditable.Id),
                nameof(IAuditable.CreationTimestampUtc)
            };

            var properties = auditableEntry.Properties
                .Where(x => excludedMetadataProperties.Contains(x.Metadata.Name) is false)
                .ToDictionary(
                    property => property.Metadata.Name,
                    property => ((string?)null, property.CurrentValue?.ToString()));

            var auditableEntity = (auditableEntry.Entity as IAuditable)!;
            var auditRecord = new AuditRecord(
                Guid.NewGuid(),
                auditableEntity.Id,
                auditableEntity.CreationTimestampUtc,
                new Actor(Guid.NewGuid(), ""),
                properties);

            auditRecords.Add(auditRecord);
        }

        await eventData.Context.AddRangeAsync(auditRecords, cancellationToken);
        return await ValueTask.FromResult(result);
    }
}
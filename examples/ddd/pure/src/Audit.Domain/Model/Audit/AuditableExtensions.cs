using Audit.Domain.Model.Vehicles;

namespace Audit.Domain.Model.Audit;

public static class AuditableExtensions
{
    public static IEnumerable<AuditRecordMetadata<T>> ToAuditRecordMetadata<T>(this IAuditable<T> auditable, T auditRecord)
        where T : IAuditRecord
    {
        var originalEntity = auditable.AuditRecords.MaxBy(x => x.Timestamp);
        if (originalEntity is null)
        {
            return new List<AuditRecordMetadata<T>> { auditRecord.CreateInitialisationMetadata() };
        }
        
        var auditRecordMetadata = new List<AuditRecordMetadata<T>>();
        foreach (var property in typeof(T).GetProperties()
                     .Where(x => x.SetMethod is not null)
                     .Where(x => x.PropertyType.IsClass is false || x.PropertyType == typeof(string)))
        {
            var originalValue = property.GetValue(originalEntity);
            var updatedValue = property.GetValue(auditable);
            if (originalValue is null && updatedValue is null) continue;
            
            if (originalValue is null || 
                updatedValue is null || 
                originalValue.Equals(updatedValue) is false)
            {
                auditRecordMetadata.Add(new AuditRecordMetadata<T>(
                    auditRecord,
                    property.Name,
                    originalValue?.ToString(),
                    updatedValue?.ToString()));
            }
        }

        return auditRecordMetadata;
    }
}
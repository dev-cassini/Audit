namespace Audit.Domain.Abstraction.Model.Audit;

public static class AuditRecordMetadataExtensions
{
    public static AuditRecordMetadata<T> CreateInitialisationMetadata<T>(this T auditRecord)
        where T : IAuditRecord
        => new(
            auditRecord,
            "CreationDateTimeUtc",
            null,
            DateTimeOffset.UtcNow.ToString());
}
using Audit.Domain.ValueObjects;

namespace Audit.Domain.Model;

public class AuditRecord
{
    public Guid Id { get; }
    public Guid ResourceId { get; }
    public DateTimeOffset TimestampUtc { get; }
    public Actor Actor { get; }

    private readonly List<AuditRecordMetadata> _metadata;
    public IEnumerable<AuditRecordMetadata> Metadata => _metadata.AsReadOnly();

    public AuditRecord(
        Guid id, 
        Guid resourceId, 
        DateTimeOffset timestampUtc, 
        Actor actor,
        Dictionary<string, (string? OriginalValue, string? UpdatedValue)> changes)
    {
        Id = id;
        ResourceId = resourceId;
        TimestampUtc = timestampUtc;
        Actor = actor;

        _metadata = changes.Select(x => new AuditRecordMetadata(
            this,
            x.Key,
            x.Value.OriginalValue,
            x.Value.UpdatedValue)).ToList();
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
#pragma warning disable CS8618, CS9264
    private AuditRecord() { }
#pragma warning restore CS8618, CS9264
    #endregion
}
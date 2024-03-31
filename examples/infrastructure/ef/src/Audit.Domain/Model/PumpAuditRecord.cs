using Audit.Domain.Abstraction.Model.Audit;
using Audit.Domain.Abstraction.Tooling;

namespace Audit.Domain.Model;

public class PumpAuditRecord : Abstraction.Model.Pump, IAuditRecord
{
    public sealed override Guid Id { get; } = Guid.NewGuid();
    public DateTimeOffset Timestamp { get; }
    public IReadOnlyCollection<AuditRecordMetadata> Metadata = null!;
    public Guid PumpId { get; }
    public Pump Pump { get; } = null!;

    public PumpAuditRecord(
        Pump pump, 
        Dictionary<string, (string? OriginalValue, string? UpdatedValue)> changes,
        IDateTimeProvider dateTimeProvider)
    : base(pump.Id, pump.LaneId, pump.VehicleId)
    {
        PumpId = pump.Id;
        Pump = pump;
        Timestamp = dateTimeProvider.UtcNow;

        var metadata = changes.Select(x => new AuditRecordMetadata(
            this,
            x.Key,
            x.Value.OriginalValue,
            x.Value.UpdatedValue)).ToArray();
        Metadata = metadata;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    private PumpAuditRecord() { }
    #endregion
}
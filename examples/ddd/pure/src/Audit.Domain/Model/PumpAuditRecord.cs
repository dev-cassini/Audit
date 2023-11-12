using Audit.Domain.Abstraction.Model.Audit;

namespace Audit.Domain.Model;

public class PumpAuditRecord : Abstraction.Model.Pump, IAuditRecord
{
    public sealed override Guid Id { get; } = Guid.NewGuid();
    public DateTimeOffset Timestamp { get; } = DateTimeOffset.UtcNow;
    public IEnumerable<AuditRecordMetadata<PumpAuditRecord>> Metadata { get; } = null!;
    public Guid PumpId { get; }
    public Pump Pump { get; } = null!;

    public PumpAuditRecord(Pump pump) : base(pump.Id, pump.Lane)
    {
        PumpId = pump.Id;
        Pump = pump;
        Metadata = pump.ToAuditRecordMetadata(this);
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    private PumpAuditRecord() { }
    #endregion
}
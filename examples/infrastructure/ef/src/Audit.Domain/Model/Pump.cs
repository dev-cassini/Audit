using Audit.Domain.Abstraction.Model.Audit;

namespace Audit.Domain.Model;

public class Pump : Abstraction.Model.Pump, IAuditable<PumpAuditRecord>
{
    public Vehicle? Vehicle { get; private set; }
    
    private readonly List<PumpAuditRecord> _auditRecords = new();
    public IEnumerable<PumpAuditRecord> AuditRecords => _auditRecords.Where(x => x.Metadata.Any());

    public Pump(Guid id, Lane lane) : base(id, lane) { }
    
    public void AddAuditRecord()
    {
        throw new NotImplementedException();
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    private Pump() { }
    #endregion
}
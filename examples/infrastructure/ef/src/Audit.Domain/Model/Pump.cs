using Audit.Domain.Abstraction.Model.Audit;
using Audit.Domain.Abstraction.Tooling;

namespace Audit.Domain.Model;

public class Pump : Abstraction.Model.Pump, IAuditable<PumpAuditRecord>
{
    public Vehicle? Vehicle { get; private set; }
    
    private readonly List<PumpAuditRecord> _auditRecords = [];
    public IEnumerable<PumpAuditRecord> AuditRecords => _auditRecords.Where(x => x.Metadata.Any());
    
    public Pump(Guid id, Lane lane) : base(id, lane.Id, null) { }
    
    public void AddAuditRecord(
        Dictionary<string, (string? OriginalValue, string? UpdatedValue)> changes,
        IDateTimeProvider dateTimeProvider)
    {
        var auditRecord = new PumpAuditRecord(this, changes, dateTimeProvider);
        _auditRecords.Add(auditRecord);
    }
    
    public void Filling(Vehicle vehicle)
    {
        VehicleId = vehicle.Id;
        Vehicle = vehicle;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    protected Pump() { }
    #endregion
}
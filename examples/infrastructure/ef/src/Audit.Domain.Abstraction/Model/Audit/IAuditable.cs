namespace Audit.Domain.Abstraction.Model.Audit;

public interface IAuditable<out T> where T : IAuditRecord
{
    IEnumerable<T> AuditRecords { get; }
    void AddAuditRecord(Dictionary<string, (string? OriginalValue, string? UpdatedValue)> changes);
}
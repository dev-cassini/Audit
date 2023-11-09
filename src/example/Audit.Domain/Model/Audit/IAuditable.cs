namespace Audit.Domain.Model.Audit;

public interface IAuditable<out T> where T : IAuditRecord
{
    IEnumerable<T> AuditRecords { get; }
    void AddAuditRecord();
}
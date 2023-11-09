namespace Audit.Domain.Model.Audit;

public interface IAuditRecord
{
    Guid Id { get; }
    DateTimeOffset Timestamp { get; }
}
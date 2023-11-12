namespace Audit.Domain.Abstraction.Model.Audit;

public interface IAuditRecord
{
    Guid Id { get; }
    DateTimeOffset Timestamp { get; }
}
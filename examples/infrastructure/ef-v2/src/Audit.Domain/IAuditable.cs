namespace Audit.Domain;

public interface IAuditable
{
    Guid Id { get; }
    DateTimeOffset CreationTimestampUtc { get; }
}
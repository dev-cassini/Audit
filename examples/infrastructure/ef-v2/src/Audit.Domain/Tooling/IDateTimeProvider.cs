namespace Audit.Domain.Tooling;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}
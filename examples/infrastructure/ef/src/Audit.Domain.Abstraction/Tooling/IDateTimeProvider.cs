namespace Audit.Domain.Abstraction.Tooling;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}
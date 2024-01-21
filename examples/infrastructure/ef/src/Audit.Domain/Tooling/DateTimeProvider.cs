using Audit.Domain.Abstraction.Tooling;

namespace Audit.Domain.Tooling;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow { get; } = DateTimeOffset.UtcNow;
}
namespace Audit.Domain.Tooling;

public class DefaultDateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
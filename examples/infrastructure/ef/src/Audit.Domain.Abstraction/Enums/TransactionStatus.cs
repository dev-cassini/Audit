namespace Audit.Domain.Abstraction.Enums;

public static class TransactionStatus
{
    public const string Waiting = "Waiting";
    public const string Filling = "Filling";
    public const string Completed = "Completed";
    public const string RageQuit = "RageQuit";
}
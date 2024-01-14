namespace Audit.Domain.Abstraction.Model;

public abstract class Forecourt
{
    public Guid Id { get; }
    
    protected readonly List<Lane> _lanes = new();
    public IEnumerable<Lane> Lanes => _lanes.AsReadOnly();
    
    protected readonly List<Transaction> _transactions = new();
    public IEnumerable<Transaction> Transactions => _transactions.AsReadOnly();

    protected Forecourt(Guid id)
    {
        Id = id;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    protected Forecourt() { }
    #endregion
}
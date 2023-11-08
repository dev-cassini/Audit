namespace Audit.Domain.Abstraction.Model;

public abstract class Forecourt
{
    public Guid Id { get; }
    
    private readonly List<Lane> _lanes = new();
    public IEnumerable<Lane> Lanes => _lanes.AsReadOnly();
    
    private readonly List<Transaction> _transactions = new();
    public IEnumerable<Transaction> Transactions => _transactions.AsReadOnly();

    protected Forecourt(Guid id)
    {
        Id = id;
    }
}
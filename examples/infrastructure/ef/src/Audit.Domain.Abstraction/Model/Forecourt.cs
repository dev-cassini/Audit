namespace Audit.Domain.Abstraction.Model;

public abstract class Forecourt
{
    public Guid Id { get; }

    protected Forecourt(Guid id)
    {
        Id = id;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    protected Forecourt() { }
    #endregion
}
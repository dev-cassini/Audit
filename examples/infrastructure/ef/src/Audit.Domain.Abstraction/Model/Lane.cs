namespace Audit.Domain.Abstraction.Model;

public abstract class Lane
{
    public Guid Id { get; }
    
    protected readonly List<Pump> _pumps = new();
    public IEnumerable<Pump> Pumps => _pumps.AsReadOnly();
    
    public Guid ForecourtId { get; }
    public Forecourt Forecourt { get; } = null!;

    protected Lane(Guid id, Forecourt forecourt)
    {
        Id = id;
        ForecourtId = forecourt.Id;
        Forecourt = forecourt;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    protected Lane() { }
    #endregion
}
namespace Audit.Domain.Model;

public class Lane : Abstraction.Model.Lane
{
    private readonly List<Pump> _pumps = new();
    public IEnumerable<Pump> Pumps => _pumps.AsReadOnly();
    
    public Lane(Guid id, Forecourt forecourt) : base(id, forecourt) { }
    
    public Pump AddPump()
    {
        var pump = new Pump(Guid.NewGuid(), this);
        _pumps.Add(pump);

        return pump;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    private Lane() { }
    #endregion
}
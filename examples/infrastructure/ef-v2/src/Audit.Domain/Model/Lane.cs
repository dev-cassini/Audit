using Audit.Domain.Tooling;

namespace Audit.Domain.Model;

public class Lane
{
    public Guid Id { get; }
    
    public Guid ForecourtId { get; }
    
    private readonly List<Pump> _pumps = [];
    public IEnumerable<Pump> Pumps => _pumps.AsReadOnly();
    
    public Lane(Guid id, Forecourt forecourt)
    {
        Id = id;
        ForecourtId = forecourt.Id;
    }
    
    public Pump AddPump(IDateTimeProvider dateTimeProvider)
    {
        var pump = new Pump(Guid.NewGuid(), this, dateTimeProvider);
        _pumps.Add(pump);

        return pump;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    private Lane() { }
    #endregion
}
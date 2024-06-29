using Audit.Domain.Common;
using Audit.Domain.Events;

namespace Audit.Domain.Model;

public class Forecourt : AggregateRoot
{
    public Guid Id { get; }
    
    private readonly List<Lane> _lanes = [];
    public IEnumerable<Lane> Lanes => _lanes.AsReadOnly();

    public Forecourt(Guid id)
    {
        Id = id;

        var @event = new ForecourtCreated(id);
        AddDomainEvent(@event);
    }
    
    public Lane AddLane()
    {
        var lane = new Lane(Guid.NewGuid(), this);
        _lanes.Add(lane);

        return lane;
    }
    
    #region EF Constructor
    private Forecourt() { }
    #endregion
}
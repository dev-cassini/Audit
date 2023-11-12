namespace Audit.Domain.Model;

public class Forecourt : Abstraction.Model.Forecourt
{
    public Forecourt(Guid id) : base(id) { }
    
    public Lane AddLane()
    {
        var lane = new Lane(Guid.NewGuid(), this);
        _lanes.Add(lane);

        return lane;
    }
}
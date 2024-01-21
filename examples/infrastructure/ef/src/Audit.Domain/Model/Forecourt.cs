namespace Audit.Domain.Model;

public class Forecourt : Abstraction.Model.Forecourt
{
    private readonly List<Lane> _lanes = new();
    public IEnumerable<Lane> Lanes => _lanes.AsReadOnly();
    
    public Forecourt(Guid id) : base(id) { }
    
    public Lane AddLane()
    {
        var lane = new Lane(Guid.NewGuid(), this);
        _lanes.Add(lane);

        return lane;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    private Forecourt() { }
    #endregion
}
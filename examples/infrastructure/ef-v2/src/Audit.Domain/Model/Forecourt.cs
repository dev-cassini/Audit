namespace Audit.Domain.Model;

public class Forecourt
{
    public Guid Id { get; }
    public string Name { get; }
    
    private readonly List<Lane> _lanes = [];
    public IEnumerable<Lane> Lanes => _lanes.AsReadOnly();

    public Forecourt(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
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
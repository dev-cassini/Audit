namespace Audit.Domain.Abstraction.Model;

public abstract class Pump
{
    /// <summary>
    /// Fuel dispense rate in litres per second.
    /// </summary>
    public const decimal FuelDispenseRate = 1.5m;
    
    public virtual Guid Id { get; }
    public Guid? VehicleId { get; protected set; }
    
    public Guid LaneId { get; }
    public Lane Lane { get; } = null!;

    protected Pump(Guid id, Lane lane)
    {
        Id = id;
        LaneId = lane.Id;
        Lane = lane;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    protected Pump() { }
    #endregion
}
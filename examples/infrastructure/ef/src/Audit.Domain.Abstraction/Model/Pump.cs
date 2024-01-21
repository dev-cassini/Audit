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

    protected Pump(Guid id, Guid laneId, Guid? vehicleId)
    {
        Id = id;
        LaneId = laneId;
        VehicleId = vehicleId;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    protected Pump() { }
    #endregion
}
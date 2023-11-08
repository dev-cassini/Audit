namespace Audit.Domain.Abstraction.Model;

public abstract class Pump
{
    /// <summary>
    /// Fuel dispense rate in litres per second.
    /// </summary>
    public const decimal FuelDispenseRate = 1.5m;
    
    public Guid Id { get; }
    public Guid? VehicleId { get; private set; }
    
    public Guid LaneId { get; }
    public Lane Lane { get; } = null!;
    
    public Pump(Guid id, Lane lane)
    {
        Id = id;
        LaneId = lane.Id;
        Lane = lane;
    }
}
namespace Audit.Domain.Model;

public class Pump
{
    /// <summary>
    /// Fuel dispense rate in litres per second.
    /// </summary>
    public const decimal FuelDispenseRate = 1.5m;
    
    public Guid Id { get; }
    public Guid? VehicleId { get; protected set; }
    public Guid LaneId { get; }
    
    public Lane Lane { get; } = null!;
    public Vehicle? Vehicle { get; private set; }

    public Pump(Guid id, Lane lane)
    {
        Id = id;
        LaneId = lane.Id;
        Lane = lane;
    }
    
    public void Filling(Vehicle vehicle)
    {
        VehicleId = vehicle.Id;
        Vehicle = vehicle;
    }
    
    #region EF Constructor
    private Pump() { }
    #endregion
}
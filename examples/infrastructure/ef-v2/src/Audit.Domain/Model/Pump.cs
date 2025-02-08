using Audit.Domain.Tooling;

namespace Audit.Domain.Model;

public class Pump : IAuditable
{
    /// <summary>
    /// Fuel dispense rate in litres per second.
    /// </summary>
    public const decimal FuelDispenseRate = 1.5m;
    
    public Guid Id { get; }
    public int Number { get; }
    public DateTimeOffset CreationTimestampUtc { get; }
    
    public Guid? VehicleId { get; private set; }
    public Vehicle? Vehicle { get; private set; }
    
    public Guid LaneId { get; }
    public Lane Lane { get; } = null!;

    public Pump(Guid id, Lane lane, IDateTimeProvider dateTimeProvider)
    {
        Id = id;
        LaneId = lane.Id;
        Lane = lane;
        Number = lane.Pumps.Count() + 1;
        CreationTimestampUtc = dateTimeProvider.UtcNow;
    }
    
    public void Filling(Vehicle vehicle)
    {
        VehicleId = vehicle.Id;
        Vehicle = vehicle;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    protected Pump() { }
    #endregion
}
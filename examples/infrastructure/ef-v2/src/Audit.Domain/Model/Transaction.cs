namespace Audit.Domain.Model;

public class Transaction
{
    /// <summary>
    /// Unique identifier.
    /// </summary>
    public Guid Id { get; }
    
    /// <summary>
    /// Status of transaction.
    /// </summary>
    public string Status { get; private set; } = null!;

    /// <summary>
    /// When the transaction was created.
    /// </summary>
    public DateTimeOffset DateTimeCreated { get; }

    /// <summary>
    /// When the transaction status moved to <see cref="TransactionStatus.Filling"/>.
    /// </summary>
    public DateTimeOffset? DateTimeFilling { get; }

    /// <summary>
    /// When the transaction status moved to <see cref="TransactionStatus.Completed"/>.
    /// </summary>
    public DateTimeOffset? DateTimeCompleted { get; private set; }

    /// <summary>
    /// Number of litres dispensed to the <see cref="Vehicle"/>.
    /// </summary>
    public int NumberOfLitresDispensed { get; private set; }
    
    public Guid VehicleId { get; }
    public Vehicle Vehicle { get; } = null!;
    public Guid? PumpId { get; }
    
    public DateTimeOffset? CompletionDateTime => DateTimeFilling?
        .AddSeconds((double)((Vehicle.TankCapacity - Vehicle.FuelLevel) / Pump.FuelDispenseRate));
    
    public Transaction(
        Guid id, 
        string status,
        DateTimeOffset dateTimeCreated,
        DateTimeOffset? dateTimeFilling,
        Vehicle vehicle, 
        Pump? pump)
    {
        Id = id;
        Status = status;
        DateTimeCreated = dateTimeCreated;
        DateTimeFilling = dateTimeFilling;
        VehicleId = vehicle.Id;
        Vehicle = vehicle;
        PumpId = pump?.Id;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    private Transaction() { }
    #endregion
}
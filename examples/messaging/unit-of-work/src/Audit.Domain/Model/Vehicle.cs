namespace Audit.Domain.Model;

public class Vehicle
{
    public virtual Guid Id { get; }
    public string Type { get; }
    public string FuelType { get; }
    public int FuelLevel { get; }
    public int TankCapacity { get; }
    
    public Vehicle(
        Guid id,
        string type,
        string fuelType,
        int fuelLevel,
        int tankCapacity)
    {
        Id = id;
        Type = type;
        FuelType = fuelType;
        FuelLevel = fuelLevel;
        TankCapacity = tankCapacity;
    }
    
    #region EF Constructor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Vehicle() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    #endregion
}
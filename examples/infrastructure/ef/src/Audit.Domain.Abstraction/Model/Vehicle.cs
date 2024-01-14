namespace Audit.Domain.Abstraction.Model;

public abstract class Vehicle
{
    public virtual Guid Id { get; }
    public string Type { get; } = null!;
    public string FuelType { get; } = null!;
    public int FuelLevel { get; set; }
    public int TankCapacity { get; }
    
    protected Vehicle(
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
    // ReSharper disable once UnusedMember.Local
    protected Vehicle() { }
    #endregion
}
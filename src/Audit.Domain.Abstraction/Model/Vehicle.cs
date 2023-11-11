using Audit.Domain.Abstraction.Enums;

namespace Audit.Domain.Abstraction.Model;

public abstract class Vehicle
{
    public virtual Guid Id { get; } = Guid.NewGuid();
    public VehicleType Type { get; }
    public FuelType FuelType { get; }
    public int FuelLevel { get; }
    public int TankCapacity { get; }
    
    protected Vehicle(
        VehicleType type, 
        FuelType fuelType, 
        int fuelLevel, 
        int tankCapacity)
    {
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
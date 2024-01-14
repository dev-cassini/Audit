namespace Audit.Domain.Model;

public class Vehicle : Abstraction.Model.Vehicle
{
    public Vehicle(
        Guid id,
        string type,
        string fuelType,
        int fuelLevel,
        int tankCapacity)
        : base(id, type, fuelType, fuelLevel, tankCapacity) { }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    private Vehicle() { }
    #endregion
}
namespace Audit.Domain.Model;

public class Transaction : Abstraction.Model.Transaction
{ 
    public Transaction(
        Guid id, 
        string status,
        DateTimeOffset dateTimeCreated,
        DateTimeOffset? dateTimeFilling,
        Vehicle vehicle, 
        Pump? pump)
        : base(
            id,
            status,
            dateTimeCreated,
            dateTimeFilling,
            vehicle,
            pump) { }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    private Transaction() { }
    #endregion
}
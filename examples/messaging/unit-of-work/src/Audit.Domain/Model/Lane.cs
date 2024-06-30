namespace Audit.Domain.Model;

public class Lane
{
    public Guid Id { get; }
    public Guid ForecourtId { get; }

    public Lane(Guid id, Forecourt forecourt)
    {
        Id = id;
        ForecourtId = forecourt.Id;
    }
    
    #region EF Constructor
    private Lane() { }
    #endregion
}
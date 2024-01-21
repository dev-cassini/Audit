namespace Audit.Domain.Abstraction.Model;

public abstract class Lane
{
    public Guid Id { get; }
    public Guid ForecourtId { get; }

    protected Lane(Guid id, Forecourt forecourt)
    {
        Id = id;
        ForecourtId = forecourt.Id;
    }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    protected Lane() { }
    #endregion
}
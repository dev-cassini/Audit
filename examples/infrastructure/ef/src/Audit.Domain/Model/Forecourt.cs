namespace Audit.Domain.Model;

public class Forecourt : Abstraction.Model.Forecourt
{
    public Forecourt(Guid id) : base(id) { }
    
    #region EF Constructor
    // ReSharper disable once UnusedMember.Local
    private Forecourt() { }
    #endregion
}
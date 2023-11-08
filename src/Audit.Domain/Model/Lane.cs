namespace Audit.Domain.Model;

public class Lane : Abstraction.Model.Lane
{
    public Lane(Guid id, Forecourt forecourt) : base(id, forecourt) { }
}
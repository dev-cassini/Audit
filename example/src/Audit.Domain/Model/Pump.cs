namespace Audit.Domain.Model;

public class Pump : Abstraction.Model.Pump
{
    public Pump(Guid id, Lane lane) : base(id, lane) { }
}
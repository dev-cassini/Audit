namespace Audit.Domain.Model;

public class Lane : Abstraction.Model.Lane
{
    public Lane(Guid id, Forecourt forecourt) : base(id, forecourt) { }
    
    public Pump AddPump()
    {
        var pump = new Pump(Guid.NewGuid(), this);
        _pumps.Add(pump);

        return pump;
    }
}
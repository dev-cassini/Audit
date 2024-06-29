using Audit.Domain.Model;
using Audit.Domain.Repositories;
using MediatR;

namespace Audit.Application.Commands.Forecourts.Create;

public class CreateForecourtCommandHandler(IForecourtRepository forecourtRepository) : IRequestHandler<CreateForecourtCommand, Guid>
{
    public async Task<Guid> Handle(CreateForecourtCommand command, CancellationToken cancellationToken)
    {
        var forecourt = new Forecourt(Guid.NewGuid());
        await forecourtRepository.AddAsync(forecourt, cancellationToken);
        
        return forecourt.Id;
    }
}
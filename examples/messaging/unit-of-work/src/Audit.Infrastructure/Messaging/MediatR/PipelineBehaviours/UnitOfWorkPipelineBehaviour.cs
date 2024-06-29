using Audit.Infrastructure.Persistence.EntityFramework;
using MediatR;

namespace Audit.Infrastructure.Messaging.MediatR.PipelineBehaviours;

public class UnitOfWorkPipelineBehaviour<TRequest, TResponse>(
    FuellerDbContext dbContext,
    IMediator mediator)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        var response = await next();

        await mediator.DispatchDomainEventsAsync(dbContext);
        await dbContext.Database.CommitTransactionAsync(cancellationToken);

        return response;
    }
}
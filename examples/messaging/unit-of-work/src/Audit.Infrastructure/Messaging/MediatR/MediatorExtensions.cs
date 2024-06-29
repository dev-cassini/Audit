using Audit.Domain.Common;
using Audit.Infrastructure.Persistence.EntityFramework;
using MediatR;

namespace Audit.Infrastructure.Messaging.MediatR;

internal static class MediatorExtensions
{
    internal static async Task DispatchDomainEventsAsync(
        this IMediator mediator, 
        FuellerDbContext dbContext)
    {
        var entityEntries = dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(x => x.Entity.DomainEvents.Any()).ToList();

        foreach (var entityEntry in entityEntries)
        {
            foreach (var domainEvent in entityEntry.Entity.DomainEvents.ToList())
            {
                entityEntry.Entity.RemoveDomainEvent(domainEvent);
                await mediator.Publish(domainEvent);
            }
        }
    }
}
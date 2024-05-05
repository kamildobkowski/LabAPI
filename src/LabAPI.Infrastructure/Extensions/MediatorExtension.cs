using LabAPI.Domain.Common;
using LabAPI.Infrastructure.Persistence;
using MediatR;

namespace LabAPI.Infrastructure.Extensions;

internal static class MediatorExtension
{
    public static async Task DispatchDomainEvents(this IMediator mediator, LabDbContext dbContext)
    {
        var domainEntities = dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(x => x.Entity.DomainEvents.Count > 0)
            .ToList();
        
        var domainEvents = domainEntities
            .SelectMany(r=>r.Entity.DomainEvents)
            .ToList();
        
        domainEntities.ForEach(r=>r.Entity.ClearDomainEvents());
        
        foreach (var @event in domainEvents)
        {
            await mediator.Publish(@event);
        }
    }
}
using System.ComponentModel.DataAnnotations.Schema;

namespace LabAPI.Domain.Common;

public abstract class AggregateRoot : Entity
{
    [NotMapped] private readonly List<IDomainEvent> _domainEvents = [];
    [NotMapped] public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
    
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
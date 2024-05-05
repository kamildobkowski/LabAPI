using LabAPI.Domain.Common;

namespace LabAPI.Domain.DomainEvents.Order;

public sealed record OrderResultsConfirmed(Entities.Order Order) : IDomainEvent
{ }
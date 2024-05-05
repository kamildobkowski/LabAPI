using LabAPI.Domain.Exceptions;
using LabAPI.Domain.Repositories;
using LabAPI.Infrastructure.Authentication.UserContext;
using MediatR;

namespace LabAPI.Application.Features.Orders.Commands;

public sealed record AcceptOrderResultsCommand(string OrderNumber) : IRequest;

internal sealed class AcceptOrderResultsCommandHandler(IOrderRepository repository, IUserContextService userContextService) 
    : IRequestHandler<AcceptOrderResultsCommand>
{
    public async Task Handle(AcceptOrderResultsCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetAsync(r=>r.OrderNumber==request.OrderNumber);
        if (entity is null)
            throw new NotFoundException();
        entity.AcceptResults(userContextService.UserId);
        repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();
    }
} 
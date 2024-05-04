using LabAPI.Domain.Exceptions;
using LabAPI.Domain.Repositories;
using MediatR;

namespace LabAPI.Application.Features.Orders.Commands;

public sealed record DeleteOrderCommand(string OrderNumber) : IRequest;

internal sealed class DeleteOrderCommandHandler(IOrderRepository repository)
	: IRequestHandler<DeleteOrderCommand>
{
	public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
	{
		var entity = await repository.GetAsync(r => r.OrderNumber == request.OrderNumber);
		if (entity is null)
			throw new NotFoundException();
		repository.DeleteAsync(entity);
		await repository.SaveChangesAsync();
	}
} 
using LabAPI.Application.Features.Orders.Dtos;
using LabAPI.Application.Features.Orders.Repository;
using MediatR;

namespace LabAPI.Application.Features.Orders.Commands;

public sealed record AddOrderResultsCommand(CreateOrderResultDto Dto) : IRequest;

internal sealed class AddOrderResultsCommandHandler(IOrderRepository repository) 
	: IRequestHandler<AddOrderResultsCommand>
{
	public async Task Handle(AddOrderResultsCommand request, CancellationToken cancellationToken)
	{
		var entity = await repository.GetAsync(r => r.OrderNumber == request.Dto.OrderNumber);
		entity!.Results = request.Dto.Results;
		await repository.Update(entity);
	}
} 
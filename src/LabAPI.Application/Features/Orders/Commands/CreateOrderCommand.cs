using AutoMapper;
using LabAPI.Application.Dtos.Orders;
using LabAPI.Application.Interfaces;
using LabAPI.Domain.Entities;
using MediatR;

namespace LabAPI.Application.Features.Orders.Commands;

public sealed record CreateOrderCommand(CreateOrderDto Dto) : IRequest;

internal sealed class CreateOrderCommandHandler(IOrderRepository repository, IMapper mapper) 
	: IRequestHandler<CreateOrderCommand>
{
	public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
	{
		var entity = mapper.Map<Order>(request.Dto);
		await repository.CreateAsync(entity);
	}
} 
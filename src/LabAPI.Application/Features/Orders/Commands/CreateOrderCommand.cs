using AutoMapper;
using LabAPI.Application.Dtos.Orders;
using LabAPI.Application.Interfaces;
using LabAPI.Domain.Entities;
using MediatR;

namespace LabAPI.Application.Features.Orders.Commands;

public sealed record CreateOrderCommand(CreateOrderDto Dto) : IRequest<string>;

internal sealed class CreateOrderCommandHandler(IOrderRepository repository, IMapper mapper) 
	: IRequestHandler<CreateOrderCommand, string>
{
	public async Task<string> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
	{
		var entity = mapper.Map<Order>(request.Dto);
		var id = await repository.CreateWithNewIdAsync(entity);
		await repository.SaveChangesAsync();
		return id;
	}
} 
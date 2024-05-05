using AutoMapper;
using LabAPI.Application.Features.Orders.Dtos;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Repositories;
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
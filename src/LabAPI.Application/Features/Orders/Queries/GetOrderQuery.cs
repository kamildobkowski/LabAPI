using AutoMapper;
using LabAPI.Application.Features.Orders.Dtos;
using LabAPI.Application.Features.Orders.Repository;
using MediatR;

namespace LabAPI.Application.Features.Orders.Queries;

public sealed record GetOrderQuery(string OrderNumber) : IRequest<OrderDto>;
internal sealed class GetOrderQueryHandler(IOrderRepository repository, IMapper mapper)
	: IRequestHandler<GetOrderQuery, OrderDto>
{
	public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
	{
		var entity = await repository.GetAsync(r => r.OrderNumber == request.OrderNumber);
		var dto = mapper.Map<OrderDto>(entity);
		return dto;
	}
} 
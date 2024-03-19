using AutoMapper;
using LabAPI.Application.Features.Orders.Dtos;
using LabAPI.Application.Features.Orders.Repository;
using LabAPI.Domain.Common;
using MediatR;

namespace LabAPI.Application.Features.Orders.Queries;

public sealed record GetAllOrderQuery(int Page, int PageSize, string? FilterBy,
	string? Filter, string? OrderBy, bool Asc) 
	: IRequest<PagedList<OrderDto>>;
	
internal sealed class GetAllOrderQueryHandler (IOrderRepository repository, IMapper mapper)
	: IRequestHandler<GetAllOrderQuery, PagedList<OrderDto>>
{
	public async Task<PagedList<OrderDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
	{
		var list = await repository.GetPageAsync(request.Page, request.PageSize, request.FilterBy,
			request.Filter, request.OrderBy, request.Asc);
		var dtos = new PagedList<OrderDto>(mapper.Map<List<OrderDto>>(list.List), list.Page, list.PageSize, list.Count);
		return dtos;
	}
}
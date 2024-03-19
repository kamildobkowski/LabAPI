using AutoMapper;
using LabAPI.Application.Features.OrderResults.Dtos;
using LabAPI.Application.Features.OrderResults.Repository;
using MediatR;

namespace LabAPI.Application.Features.OrderResults.Queries;

public sealed record GetOrderResultQuery(string OrderNumber) : IRequest<OrderResultDto>;

internal sealed class GetOrderResultQueryHandler(IOrderResultRepository repository, IMapper mapper) 
	: IRequestHandler<GetOrderResultQuery, OrderResultDto>
{
	public async Task<OrderResultDto> Handle(GetOrderResultQuery request, CancellationToken cancellationToken)
	{
		var entity = await repository.GetAsync(r => r.OrderNumber == request.OrderNumber);
		var dto = mapper.Map<OrderResultDto>(entity);
		return dto;
	}
}
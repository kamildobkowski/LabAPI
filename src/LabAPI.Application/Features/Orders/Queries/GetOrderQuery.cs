using AutoMapper;
using LabAPI.Application.Features.Orders.Dtos;
using LabAPI.Domain.Exceptions;
using LabAPI.Domain.Repositories;
using MediatR;

namespace LabAPI.Application.Features.Orders.Queries;

public sealed record GetOrderQuery(string OrderNumber) : IRequest<OrderDto>;
internal sealed class GetOrderQueryHandler(IOrderRepository repository, ITestRepository testRepository, IMapper mapper)
	: IRequestHandler<GetOrderQuery, OrderDto>
{
	public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
	{
		var entity = await repository.GetAsync(r => r.OrderNumber == request.OrderNumber);
		if (entity is null)
			throw new NotFoundException();
		var dto = mapper.Map<OrderDto>(entity);
		foreach (var i in entity.Results.Keys)
		{
			var test = await testRepository.GetAsync(r=>r.ShortName == i);
			var testDto = new OrderDto.TestsWithResultsDto
			{
				Id = test!.Id,
				Name = test.Name,
				ShortName = test.ShortName
			};
			foreach (var marker in test.Markers)
			{
				var markerDto = new OrderDto.TestsWithResultsDto.MarkerWithResultDto
				{
					Name = marker.Name,
					LowerNorm = marker.LowerNorm.ToString(),
					HigherNorm = marker.HigherNorm.ToString(),
					Unit = marker.Unit,
					ShortName = marker.ShortName,
					Result = entity.Results[i]![marker.ShortName]
				};
				testDto.Markers.Add(markerDto);
			}
			dto.Tests.Add(testDto);
		}
		return dto;
	}
} 
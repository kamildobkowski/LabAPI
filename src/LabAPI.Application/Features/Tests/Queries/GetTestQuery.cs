using AutoMapper;
using LabAPI.Application.Dtos.Tests;
using LabAPI.Application.Interfaces;
using LabAPI.Domain.Exceptions;
using MediatR;

namespace LabAPI.Application.Features.Tests.Queries;

public sealed record GetTestQuery(string Id) : IRequest<TestDto>;

internal sealed class GetTestFromShortNameQueryHandler(IMapper mapper, ITestRepository repository)
	: IRequestHandler<GetTestQuery, TestDto>
{
	public async Task<TestDto> Handle(GetTestQuery request, CancellationToken cancellationToken)
	{
		var entity = await repository.GetAsync(r => r.Id == request.Id);
		if (entity is null)
			throw new NotFoundException("Test not found");
		var dto = mapper.Map<TestDto>(entity);
		return dto;
	}
} 
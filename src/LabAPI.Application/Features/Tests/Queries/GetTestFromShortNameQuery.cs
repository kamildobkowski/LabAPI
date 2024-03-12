using AutoMapper;
using LabAPI.Application.Dtos.Tests;
using LabAPI.Application.Interfaces;
using LabAPI.Domain.Exceptions;
using MediatR;

namespace LabAPI.Application.Features.Tests.Queries;

public sealed record GetTestFromShortNameQuery(string ShortName) : IRequest<TestDto>;

internal sealed class GetTestFromShortNameQueryHandler(IMapper mapper, ITestRepository repository)
	: IRequestHandler<GetTestFromShortNameQuery, TestDto>
{
	public async Task<TestDto> Handle(GetTestFromShortNameQuery request, CancellationToken cancellationToken)
	{
		var entity = await repository.GetAsync(r => r.ShortName.Equals(request.ShortName, StringComparison.CurrentCultureIgnoreCase));
		if (entity is null)
			throw new NotFoundException("Test not found");
		var dto = mapper.Map<TestDto>(entity);
		return dto;
	}
} 
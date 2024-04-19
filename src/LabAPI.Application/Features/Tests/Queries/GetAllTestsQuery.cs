using AutoMapper;
using LabAPI.Application.Dtos.Tests;
using LabAPI.Domain.Common;
using LabAPI.Domain.Repositories;
using MediatR;

namespace LabAPI.Application.Features.Tests.Queries;

public sealed record GetAllTestsQuery(int Page, int PageSize, 
	string? Filter, string? OrderBy, 
	bool SortOrder) : IRequest<PagedList<TestDto>>;
internal sealed class GetAllTestsQueryHandler (ITestRepository repository, IMapper mapper)
	: IRequestHandler<GetAllTestsQuery, PagedList<TestDto>>
{
	public async Task<PagedList<TestDto>> Handle(GetAllTestsQuery request, CancellationToken cancellationToken)
	{
		var entitiesPagedList = await repository
			.GetPageAsync(request.Page, request.PageSize, request.Filter,
				request.OrderBy, request.SortOrder);
		var dtoPagedList =
			new PagedList<TestDto>(
				mapper.Map<List<TestDto>>(entitiesPagedList.List), entitiesPagedList.Page,
				entitiesPagedList.PageSize, entitiesPagedList.Count, entitiesPagedList.AllItemsCount);
		return dtoPagedList;
	}
}
	
using LabAPI.Application.Features.Accounts.Dtos;
using LabAPI.Domain.Common;
using LabAPI.Domain.Repositories;
using MediatR;

namespace LabAPI.Application.Features.Accounts.Queries;

public sealed record GetAllWorkersQuery(int Page, int PageSize, string? Filter, string? OrderBy, bool? SortOrder) 
    : IRequest<PagedList<WorkerDto>>;

internal sealed class GetAllWorkersQueryHandler(IWorkerRepository repository) : 
    IRequestHandler<GetAllWorkersQuery, PagedList<WorkerDto>>
{
    public async Task<PagedList<WorkerDto>> Handle(GetAllWorkersQuery request, CancellationToken cancellationToken)
    {
        var pagedList = await repository.GetPageAsync(
            request.Page,
            request.PageSize,
            request.Filter,
            request.OrderBy,
            request.SortOrder ?? true);
        
        var pagedDtoList = new PagedList<WorkerDto>(
            pagedList.List.Select(r => new WorkerDto
            {
                FullName = $"{r.Name} {r.Surname}",
                Email = r.Email,
                Role = r.Role.ToString()
            }).ToList(),
            pagedList.Page,
            pagedList.PageSize,
            pagedList.Count,
            pagedList.AllItemsCount);
        
        return pagedDtoList;
    }
}
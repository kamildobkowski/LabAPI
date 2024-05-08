using AutoMapper;
using LabAPI.Application.Features.Tests.Dtos;
using LabAPI.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace LabAPI.Application.Features.Tests.Queries;

public sealed record GetAllTestsNoMarkersQuery(string? Filter) : IRequest<List<TestNoMarkersDto>>;

internal sealed class GetAllTestsNoMarkersQueryHandler(ITestRepository repository, IMapper mapper) 
    : IRequestHandler<GetAllTestsNoMarkersQuery, List<TestNoMarkersDto>>
{
    public async Task<List<TestNoMarkersDto>> Handle(GetAllTestsNoMarkersQuery request, CancellationToken cancellationToken)
    {
        var filterToLower = request.Filter?.ToLower();
        var list = await repository
            .GetAllAsync(r=>
                filterToLower == null ||
                filterToLower.IsNullOrEmpty() ||
                r.ShortName.ToLower().Contains(filterToLower) ||
                r.Name.ToLower().Contains(filterToLower)
                );
        var dtos = mapper.Map<List<TestNoMarkersDto>>(list);
        return dtos;
    }
}
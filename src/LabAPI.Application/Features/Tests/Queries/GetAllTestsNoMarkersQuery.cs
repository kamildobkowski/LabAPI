using AutoMapper;
using LabAPI.Application.Features.Tests.Dtos;
using LabAPI.Domain.Repositories;
using MediatR;

namespace LabAPI.Application.Features.Tests.Queries;

public sealed record GetAllTestsNoMarkersQuery : IRequest<List<TestNoMarkersDto>>;

internal sealed class GetAllTestsNoMarkersQueryHandler(ITestRepository repository, IMapper mapper) 
    : IRequestHandler<GetAllTestsNoMarkersQuery, List<TestNoMarkersDto>>
{
    public async Task<List<TestNoMarkersDto>> Handle(GetAllTestsNoMarkersQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync();
        var dtos = mapper.Map<List<TestNoMarkersDto>>(list);
        return dtos;
    }
}
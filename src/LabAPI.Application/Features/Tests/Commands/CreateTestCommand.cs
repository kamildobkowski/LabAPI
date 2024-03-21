using AutoMapper;
using LabAPI.Application.Dtos.Tests;
using LabAPI.Application.Features.Tests.Repository;
using LabAPI.Domain.Entities;
using MediatR;

namespace LabAPI.Application.Features.Tests.Commands;

public sealed record CreateTestCommand(CreateTestDto Dto) : IRequest;

internal sealed class CreateTestCommandHandler(ITestRepository repository, IMapper mapper) 
	: IRequestHandler<CreateTestCommand>
{
	public async Task Handle(CreateTestCommand request, CancellationToken cancellationToken)
	{
		var entity = mapper.Map<Test>(request.Dto);
		await repository.CreateAsync(entity);
	}
} 
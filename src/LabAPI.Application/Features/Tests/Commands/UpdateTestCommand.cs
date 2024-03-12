using AutoMapper;
using LabAPI.Application.Dtos.Tests;
using LabAPI.Application.Interfaces;
using LabAPI.Domain.Entities;
using MediatR;

namespace LabAPI.Application.Features.Tests.Commands;

public sealed record UpdateTestCommand(string Id, CreateTestDto Dto) : IRequest;
internal sealed class UpdateTestCommandHandler(ITestRepository repository, IMapper mapper)
	: IRequestHandler<UpdateTestCommand>
{
	public async Task Handle(UpdateTestCommand request, CancellationToken cancellationToken)
	{
		var entity = mapper.Map<Test>(request.Dto);
		await repository.UpdateAsync(entity);
	}
}
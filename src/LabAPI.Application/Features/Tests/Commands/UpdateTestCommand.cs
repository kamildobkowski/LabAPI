using AutoMapper;
using LabAPI.Application.Features.Tests.Dtos;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Repositories;
using MediatR;

namespace LabAPI.Application.Features.Tests.Commands;

public sealed record UpdateTestCommand(string Id, UpdateTestDto Dto) : IRequest;
internal sealed class UpdateTestCommandHandler(ITestRepository repository, IMapper mapper)
	: IRequestHandler<UpdateTestCommand>
{
	public async Task Handle(UpdateTestCommand request, CancellationToken cancellationToken)
	{
		var entity = mapper.Map<Test>(request.Dto);
		repository.UpdateAsync(entity);
		await repository.SaveChangesAsync();
	}
}
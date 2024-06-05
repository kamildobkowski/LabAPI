using AutoMapper;
using LabAPI.Application.Features.Tests.Dtos;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Exceptions;
using LabAPI.Domain.Repositories;
using MediatR;

namespace LabAPI.Application.Features.Tests.Commands;

public sealed record UpdateTestCommand(string Id, UpdateTestDto Dto) : IRequest;
internal sealed class UpdateTestCommandHandler(ITestRepository repository, IMapper mapper)
	: IRequestHandler<UpdateTestCommand>
{
	public async Task Handle(UpdateTestCommand request, CancellationToken cancellationToken)
	{
		var entity = await repository.GetAsync(r=>r.Id == request.Id);
		if (entity is null)
			throw new NotFoundException();
		var newEntity  = mapper.Map<Test>(request.Dto);
		entity.ShortName = newEntity.ShortName;
		entity.Name = newEntity.Name;
		foreach (var newMarker in newEntity.Markers)
		{
			var existingMarker = entity.Markers.FirstOrDefault(r=>r.ShortName == newMarker.ShortName);

			if (existingMarker is not null)
			{
				existingMarker.Name = newMarker.Name;
				existingMarker.Unit = newMarker.Unit;
				existingMarker.LowerNorm = newMarker.LowerNorm;
				existingMarker.HigherNorm = newMarker.HigherNorm;
			}
			else
			{
				entity.Markers.Add(newMarker);
			}
		}
		repository.UpdateAsync(entity);
		await repository.SaveChangesAsync();
	}
}
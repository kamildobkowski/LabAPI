using LabAPI.Domain.Exceptions;
using LabAPI.Domain.Repositories;
using MediatR;

namespace LabAPI.Application.Features.Tests.Commands;

public sealed record DeleteTestCommand (string Id) : IRequest;

internal sealed class DeleteTestCommandHandler(ITestRepository repository) 
	: IRequestHandler<DeleteTestCommand>
{
	public async Task Handle(DeleteTestCommand request, CancellationToken cancellationToken)
	{
		var entity = await repository.GetAsync(r => r.Id == request.Id);
		if (entity is null)
			throw new NotFoundException();
		repository.DeleteAsync(entity);
		await repository.SaveChangesAsync();
	}
} 
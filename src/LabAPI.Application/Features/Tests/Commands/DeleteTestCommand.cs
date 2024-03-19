using LabAPI.Application.Features.Tests.Repository;
using LabAPI.Domain.Exceptions;
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
		await repository.Delete(entity);
		await repository.SaveChangesAsync();
	}
} 
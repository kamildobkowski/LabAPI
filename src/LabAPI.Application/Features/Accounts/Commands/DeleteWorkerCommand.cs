using LabAPI.Domain.Exceptions;
using LabAPI.Domain.Repositories;
using MediatR;

namespace LabAPI.Application.Features.Accounts.Commands;

public sealed record DeleteWorkerCommand(string Email) : IRequest;

internal sealed class DeleteWorkerCommandHandler(IWorkerRepository repository) 
    : IRequestHandler<DeleteWorkerCommand>
{
    public async Task Handle(DeleteWorkerCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetAsync(r=>r.Email==request.Email);
        if (entity is null)
            throw new NotFoundException();
        repository.DeleteAsync(entity);
        await repository.SaveChangesAsync();
    }
} 
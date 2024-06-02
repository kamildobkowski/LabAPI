using LabAPI.Domain.Entities;
using LabAPI.Domain.Exceptions;
using LabAPI.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LabAPI.Application.Features.Accounts.Commands;

public sealed record ResetWorkerPasswordCommand(string Email) : IRequest<string>;

internal sealed class ResetWorkerPasswordCommandHandler(IWorkerRepository repository, IPasswordHasher<Worker> hasher)
    : IRequestHandler<ResetWorkerPasswordCommand, string>
{
    public async Task<string> Handle(ResetWorkerPasswordCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetAsync(r=>r.Email==request.Email);
        if (entity is null)
            throw new NotFoundException();
        var generatedPassword = User.GeneratePassword();
        entity.PasswordHash = hasher.HashPassword(entity, generatedPassword);
        await repository.SaveChangesAsync();
        return generatedPassword;
    }
}
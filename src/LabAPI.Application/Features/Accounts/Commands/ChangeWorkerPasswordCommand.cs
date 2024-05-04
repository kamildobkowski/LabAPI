using LabAPI.Application.Features.Accounts.Dtos;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Exceptions;
using LabAPI.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LabAPI.Application.Features.Accounts.Commands;

public sealed record ChangeWorkerPasswordCommand(ChangeWorkerPasswordDto Dto) : IRequest;

internal sealed class ChangeWorkerPasswordCommandHandler (IWorkerRepository workerRepository, 
	IPasswordHasher<User> passwordHasher) 
	: IRequestHandler<ChangeWorkerPasswordCommand>
{
	public async Task Handle(ChangeWorkerPasswordCommand request, CancellationToken cancellationToken)
	{
		var entity = await workerRepository.GetAsync(r => r.Email == request.Dto.Email);
		if (entity is null)
			throw new NotFoundException();
		entity.PasswordHash = passwordHasher.HashPassword(entity, request.Dto.NewPassword);
		workerRepository.UpdateAsync(entity);
		await workerRepository.SaveChangesAsync();
	}
} 
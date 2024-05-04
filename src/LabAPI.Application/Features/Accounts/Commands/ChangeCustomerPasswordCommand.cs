using LabAPI.Application.Features.Accounts.Dtos;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Exceptions;
using LabAPI.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LabAPI.Application.Features.Accounts.Commands;

public sealed record ChangeCustomerPasswordCommand(ChangeCustomerPasswordDto Dto) : IRequest;

internal sealed class ChangeCustomerPasswordCommandHandler(ICustomerRepository repository, 
	IPasswordHasher<User> passwordHasher) 
	: IRequestHandler<ChangeCustomerPasswordCommand>
{
	public async Task Handle(ChangeCustomerPasswordCommand request, CancellationToken cancellationToken)
	{
		var entity = await repository.GetAsync(r => r.Email == request.Dto.Email);
		if (entity is null)
			throw new UnauthorizedException();
		entity.PasswordHash = passwordHasher.HashPassword(entity, request.Dto.NewPassword);
		repository.UpdateAsync(entity);
		await repository.SaveChangesAsync();
	}
} 
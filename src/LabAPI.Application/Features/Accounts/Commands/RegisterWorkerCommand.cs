using AutoMapper;
using LabAPI.Application.Features.Accounts.Dtos;
using LabAPI.Application.Features.Accounts.Repository;
using LabAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LabAPI.Application.Features.Accounts.Commands;

public sealed record RegisterWorkerCommand(RegisterWorkerDto Dto) : IRequest<WorkerWithPasswordDto>;

internal sealed class RegisterWorkerCommandHandler(IWorkerRepository repository, IMapper mapper, IPasswordHasher<User> passwordHasher)
	: IRequestHandler<RegisterWorkerCommand, WorkerWithPasswordDto>
{
	public async Task<WorkerWithPasswordDto> Handle(RegisterWorkerCommand request, CancellationToken cancellationToken)
	{
		var entity = mapper.Map<Worker>(request.Dto);
		var generatedPassword = User.GeneratePassword();
		entity.PasswordHash = passwordHasher.HashPassword(entity, generatedPassword);
		var dto = new WorkerWithPasswordDto(
			entity.Email, generatedPassword, entity.Role.ToString(), entity.Name, entity.Surname);
		await repository.CreateAsync(entity);
		return dto;
	}
}
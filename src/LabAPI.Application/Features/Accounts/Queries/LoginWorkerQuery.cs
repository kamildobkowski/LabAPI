using LabAPI.Application.Common.Interfaces;
using LabAPI.Application.Features.Accounts.Dtos;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Exceptions;
using LabAPI.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LabAPI.Application.Features.Accounts.Queries;

public sealed record LoginWorkerQuery(LoginDto Dto) : IRequest<string>;

internal sealed class LoginWorkerQueryHandler(IWorkerRepository workerRepository, IPasswordHasher<User> passwordHasher,
	IJwtService jwtService)
	: IRequestHandler<LoginWorkerQuery, string>
{
	public async Task<string> Handle(LoginWorkerQuery request, CancellationToken cancellationToken)
	{
		var entity = await workerRepository.GetAsync(r => r.Email == request.Dto.Email);
		if (entity is null)
			throw new UnauthorizedException();
		var result = passwordHasher.VerifyHashedPassword(
			entity, entity.PasswordHash, request.Dto.Password);
		if (result == PasswordVerificationResult.Failed)
			throw new UnauthorizedException();
		var token = jwtService.GenerateToken(entity);
		return token;
	}
}
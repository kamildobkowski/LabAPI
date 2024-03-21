using AutoMapper;
using LabAPI.Application.Features.Accounts.Dtos;
using LabAPI.Application.Features.Accounts.Repository;
using LabAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LabAPI.Application.Features.Accounts.Commands;

public sealed record RegisterCustomerCommand(RegisterCustomerDto Dto) : IRequest;

internal sealed class RegisterCustomerCommandHandler(ICustomerRepository customerRepository, 
	IPasswordHasher<Customer> passwordHasher, IMapper mapper)
	: IRequestHandler<RegisterCustomerCommand>
{
	public async Task Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
	{
		var entity = mapper.Map<Customer>(request.Dto);
		entity.PasswordHash = passwordHasher.HashPassword(entity, request.Dto.Password);
		await customerRepository.Create(entity);
	}
} 
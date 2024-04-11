using FluentValidation;
using LabAPI.Domain.Enums;
using LabAPI.Domain.Repositories;

namespace LabAPI.Application.Features.Accounts.Dtos;

public sealed record RegisterWorkerDto(string Email, string Name, string Surname, string UserRole);

public sealed class RegisterWorkerDtoValidator 
	: AbstractValidator<RegisterWorkerDto>
{
	public RegisterWorkerDtoValidator(IWorkerRepository workerRepository)
	{
		RuleFor(r => r.Email)
			.NotEmpty()
			.EmailAddress()
			.Custom(((s, context) =>
			{
				var entity = workerRepository.GetAsync(r => r.Email == s).Result;
				if(entity is not null) context.AddFailure("Email is already in use");
			}));
		RuleFor(r => r.Name)
			.NotEmpty()
			.Matches(@"^[A-Za-z]+$")
			.Matches(@"^(?:[A-Z][a-z]*\s?){1,2}$");
		RuleFor(r=>r.Surname)
			.NotEmpty()
			.Matches(@"^[A-Za-z]+$")
			.Matches(@"^(?:[A-Z][a-z]*\s?){1,2}$");
		RuleFor(r => r.UserRole)
			.NotEmpty()
			.Custom(((s, context) =>
			{
				if (Enum.TryParse(s, ignoreCase: true, out UserRole _)) return;
				context.AddFailure("invalid role");
			}));
	}
}
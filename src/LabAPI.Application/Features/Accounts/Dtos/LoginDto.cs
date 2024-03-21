using FluentValidation;

namespace LabAPI.Application.Features.Accounts.Dtos;

public sealed record LoginDto(string Email, string Password);

public sealed class LoginDtoValidator : AbstractValidator<LoginDto>
{
	public LoginDtoValidator()
	{
		RuleFor(r => r.Email)
			.EmailAddress()
			.NotEmpty();
		RuleFor(r => r.Password)
			.NotEmpty()
			.MinimumLength(8);
	}
}
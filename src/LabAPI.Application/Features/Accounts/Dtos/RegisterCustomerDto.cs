using FluentValidation;
using LabAPI.Application.Features.Accounts.Repository;

namespace LabAPI.Application.Features.Accounts.Dtos;

public sealed record RegisterCustomerDto
{
	public string Email { get; init; } = null!;
	public string Password { get; init; } = null!;
	public string RepeatPassword { get; init; } = null!;
	public string Name { get; init; } = null!;
	public string Surname { get; init; } = null!;
	public string Pesel { get; init; } = null!;
}

public sealed class RegisterCustomerDtoValidation : AbstractValidator<RegisterCustomerDto>
{
	public RegisterCustomerDtoValidation(ICustomerRepository customerRepository)
	{
		RuleFor(r => r.Email)
			.NotEmpty()
			.EmailAddress()
			.Custom((s, c) =>
			{
				var entity = customerRepository.GetAsync(r => r.Email == s).Result;
				if(entity is not null)
					c.AddFailure("Email is already linked to different account");
			});
		RuleFor(r => r.Password)
			.NotEmpty()
			.MinimumLength(8)
			.Equal(r => r.RepeatPassword)
			.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$");
		RuleFor(r => r.Pesel)
			.NotEmpty()
			.Length(11)
			.Custom((s, context) =>
			{
				var entity = customerRepository.GetAsync(r => r.Pesel == s).Result;
				if (entity is not null)
					context.AddFailure("Pesel is already linked to different account");
			});
		RuleFor(r => r.Name)
			.NotEmpty()
			.Matches(@"^[A-Za-z]+$")
			.Matches(@"^(?:[A-Z][a-z]*\s?){1,2}$");
		RuleFor(r=>r.Surname)
			.NotEmpty()
			.Matches(@"^[A-Za-z]+$")
			.Matches(@"^(?:[A-Z][a-z]*\s?){1,2}$");
	}
}
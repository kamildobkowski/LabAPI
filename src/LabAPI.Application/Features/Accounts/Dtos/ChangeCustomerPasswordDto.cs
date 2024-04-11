using FluentValidation;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace LabAPI.Application.Features.Accounts.Dtos;

public sealed record ChangeCustomerPasswordDto(string Email, string OldPassword, string NewPassword, string RepeatPassword);

public sealed class ChangeCustomerPasswordDtoValidator : AbstractValidator<ChangeWorkerPasswordDto>
{
	public ChangeCustomerPasswordDtoValidator(ICustomerRepository repository, IPasswordHasher<User> passwordHasher)
	{
		RuleFor(r => r.Email)
			.NotEmpty()
			.EmailAddress()
			.Custom((s, context) =>
			{
				var entity = repository.GetAsync(r => r.Email == s).Result;
				if(entity is null)
					context.AddFailure("Account with given email doesn't exist");
			});
		RuleFor(r => r.NewPassword)
			.NotEmpty()
			.MinimumLength(8)
			.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).*$")
			.Equal(r => r.RepeatPassword);
		RuleFor(r => new {r.Email, r.OldPassword})
			.NotEmpty()
			.Custom((s, context) =>
			{
				var entity = repository.GetAsync(r => r.Email == s.Email).Result;
				if (entity is null)
				{
					context.AddFailure("Account with given email doesn't exist");
					return;
				}
				var result = passwordHasher.VerifyHashedPassword(entity!, entity!.PasswordHash, s.OldPassword);
				if(result == PasswordVerificationResult.Failed)
					context.AddFailure("Wrong password");
			});
	}
}
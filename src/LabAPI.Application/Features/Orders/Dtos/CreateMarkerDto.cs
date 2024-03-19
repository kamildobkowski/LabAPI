using FluentValidation;

namespace LabAPI.Application.Features.Orders.Dtos;

public sealed record CreateMarkerDto(string Name,
	decimal? LowerNorm,
	decimal? HigherNorm,
	string? Unit,
	string? ShortName);

public sealed class CreateMarkerDtoValidator : AbstractValidator<CreateMarkerDto>
{
	public CreateMarkerDtoValidator()
	{
		RuleFor(r => r.Name)
			.NotEmpty()
			.MaximumLength(100)
			.MinimumLength(2);
	}
}
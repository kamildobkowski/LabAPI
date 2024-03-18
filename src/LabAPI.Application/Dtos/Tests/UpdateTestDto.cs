using FluentValidation;
using LabAPI.Application.Dtos.Markers;
using LabAPI.Application.Interfaces;
using LabAPI.Domain.Extensions;

namespace LabAPI.Application.Dtos.Tests;

public record UpdateTestDto
{
	public string? ShortName { get; init; }
	public string Name { get; init; } = null!;
	public List<CreateMarkerDto> Markers { get; init; } = null!;
}

public sealed class UpdateTestDtoValidator : AbstractValidator<CreateTestDto>
{
	public UpdateTestDtoValidator(ITestRepository repository)
	{
		RuleFor(r => r.Name)
			.NotEmpty()
			.MaximumLength(100)
			.MinimumLength(2);
		RuleFor(r => r.ShortName)
			.MaximumLength(100)
			.MinimumLength(2);
	}
}
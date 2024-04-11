using FluentValidation;
using LabAPI.Application.Features.Orders.Dtos;
using LabAPI.Domain.Extensions;
using LabAPI.Domain.Repositories;

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
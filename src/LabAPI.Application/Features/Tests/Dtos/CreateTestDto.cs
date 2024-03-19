using FluentValidation;
using LabAPI.Application.Features.Orders.Dtos;
using LabAPI.Application.Features.Tests.Repository;
using LabAPI.Domain.Extensions;

namespace LabAPI.Application.Dtos.Tests;

public sealed record CreateTestDto
{
	public string? ShortName { get; init; }
	public string Name { get; init; } = null!;
	public List<CreateMarkerDto> Markers { get; init; } = null!;
}

public sealed class CreateTestDtoValidator : AbstractValidator<CreateTestDto>
{
	public CreateTestDtoValidator(ITestRepository repository)
	{
		RuleFor(r => r.Name)
			.NotEmpty()
			.MaximumLength(100)
			.MinimumLength(2);
		RuleFor(r => r.ShortName)
			.MaximumLength(100)
			.MinimumLength(2);
		RuleFor(r => new { r.ShortName, r.Name })
			.Custom((r, context) =>
			{
				var id = r.ShortName ?? r.Name;
				id = id.EncodePolishLetterAndWhiteChars();
				var entity = repository.GetAsync(r => r.Id == id).Result;
				if (entity is not null)
					context.AddFailure("entity with given shortName already exists");
			});
	}
}
using LabAPI.Application.Dtos.Markers;

namespace LabAPI.Application.Dtos.Tests;

public sealed record CreateTestDto
{
	public string? ShortName { get; init; }
	public string Name { get; init; } = null!;
	public List<CreateMarkerDto> Markers { get; init; } = null!;
}
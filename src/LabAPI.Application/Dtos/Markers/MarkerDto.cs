namespace LabAPI.Application.Dtos.Markers;

public sealed record MarkerDto(
	string Name,
	decimal LowerNorm,
	decimal HigherNorm,
	string? Unit = null,
	string? ShortName = null);
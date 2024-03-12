namespace LabAPI.Application.Dtos.Markers;

public sealed record CreateMarkerDto(string Name,
	decimal LowerNorm,
	decimal HigherNorm,
	string? Unit,
	string? ShortName);
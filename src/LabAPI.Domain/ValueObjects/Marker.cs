using LabAPI.Domain.Common;

namespace LabAPI.Domain.ValueObjects;

public sealed class Marker(
	string name,
	decimal lowerNorm,
	decimal higherNorm,
	string? unit = null,
	string? shortName = null)
{
	public string ShortName { get; set; } = shortName ?? name;
	public string Name { get; set; } = name;
	public decimal LowerNorm { get; set; } = lowerNorm;
	public decimal HigherNorm { get; set; } = higherNorm;
	public string? Unit { get; set; } = unit;
}
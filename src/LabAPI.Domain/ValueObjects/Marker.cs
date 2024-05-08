using LabAPI.Domain.Common;

namespace LabAPI.Domain.ValueObjects;

public sealed class Marker : ValueObject
{
	public Marker() { }
	public Marker(string name,
		decimal? lowerNorm = null,
		decimal? higherNorm = null,
		string? unit = null,
		string? shortName = null)
	{
		ShortName = shortName is null or "" ? name : shortName;
		Name = name;
		LowerNorm = lowerNorm;
		HigherNorm = higherNorm;
		Unit = unit;
	}

	public string ShortName { get; set; } = null!;
	public string Name { get; set; } = null!;
	public decimal? LowerNorm { get; set; }
	public decimal? HigherNorm { get; set; }
	public string? Unit { get; set; }
}
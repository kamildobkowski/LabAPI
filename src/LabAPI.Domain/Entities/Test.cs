using System.Runtime.CompilerServices;
using LabAPI.Domain.Common;

namespace LabAPI.Domain.Entities;

public sealed class Test(
	string name,
	decimal lowerNorm,
	decimal higherNorm,
	string? unit = null,
	string? shortName = null)
	: BaseEntity
{
	public string ShortName { get; set; } = shortName ?? name;
	public string Name { get; set; } = name;
	public decimal LowerNorm { get; set; } = lowerNorm;
	public decimal HigherNorm { get; set; } = higherNorm;
	public string? Unit { get; set; } = unit;
}
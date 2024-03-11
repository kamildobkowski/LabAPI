using LabAPI.Domain.Common;

namespace LabAPI.Domain.Entities;

public sealed class Test : BaseEntity
{
	public string? Result { get; set; }
	public decimal LowerNorm { get; set; }
	public decimal HigherNorm { get; set; }
	public string? Unit { get; set; }
}
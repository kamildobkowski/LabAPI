namespace LabAPI.Domain.Entities;

public sealed class Test
{
	public string Id { get; set; }
	public string? Result { get; set; }
	public decimal LowerNorm { get; set; }
	public decimal HigherNorm { get; set; }
	public string? Unit { get; set; }
}
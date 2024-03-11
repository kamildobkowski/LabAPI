using LabAPI.Domain.Common;

namespace LabAPI.Domain.Entities;

public sealed class TestResult : BaseEntity
{
	public string TestId { get; set; } = null!;
	public Dictionary<string, Dictionary<string, string>> Results { get; set; } = new();
}
using LabAPI.Domain.Common;

namespace LabAPI.Domain.Entities;

public sealed class TestPanel(string name, string? shortName = null) : BaseEntity
{
	public string ShortName { get; set; } = shortName ?? name;
	public string Name { get; set; } = name;
	public List<Test> Tests { get; set; } = [];
}
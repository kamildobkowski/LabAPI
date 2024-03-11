using LabAPI.Domain.Common;

namespace LabAPI.Domain.Entities;

public sealed class TestPanel : BaseEntity
{
	public string Name { get; set; } = null!;
	public List<Test> Tests { get; set; } = null!;
}
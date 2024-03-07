namespace LabAPI.Domain.Entities;

public sealed class TestPanel
{
	public string Id { get; set; } = null!;
	public string Name { get; set; } = null!;
	public List<Test> Tests { get; set; } = null!;
}
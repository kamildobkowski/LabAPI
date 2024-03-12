using LabAPI.Domain.Common;
using LabAPI.Domain.ValueObjects;

namespace LabAPI.Domain.Entities;

public sealed class Test(string name, string? shortName = null) : BaseEntity
{
	public string ShortName { get; set; } = shortName ?? name;
	public string Name { get; set; } = name;
	public List<Marker> Markers { get; set; } = [];
}
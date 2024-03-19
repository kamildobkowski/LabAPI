using LabAPI.Domain.Common;
using LabAPI.Domain.Extensions;
using LabAPI.Domain.ValueObjects;

namespace LabAPI.Domain.Entities;

public sealed class Test : BaseEntity
{
	public string ShortName { get; set; }
	public string Name { get; set; }
	public List<Marker> Markers { get; set; } = [];

	public Test(string name, string? shortName = null)
	{
		Name = name;
		ShortName = shortName ?? name;
		Id = ShortName.EncodePolishLetterAndWhiteChars();
	}
}
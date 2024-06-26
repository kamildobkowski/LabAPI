using LabAPI.Domain.Common;
using LabAPI.Domain.Extensions;
using LabAPI.Domain.ValueObjects;

namespace LabAPI.Domain.Entities;

public sealed class Test : AggregateRoot
{
	public string ShortName { get; set; } = null!;
	public string Name { get; set; } = null!;
	public List<Marker> Markers { get; set; } = [];

	public Test(string name, string? shortName = null)
	{
		Name = name;
		ShortName = shortName ?? name;
		Id = ShortName.EncodePolishLetterAndWhiteChars();
	}

	public Test() { }
	
	public bool FilterByNameAndShortName(string? filter)
	{
		if (filter is null or "") return true;
		var fullTestName = ShortName.ToLower() + ' ' + Name.ToLower();
		var splitFilter = filter.Split(' ');
		foreach (var s in splitFilter)
		{
			if (fullTestName.Contains(s.ToLower()))
				return true;
		}
		return false;
	}
}
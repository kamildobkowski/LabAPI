using LabAPI.Domain.Common;

namespace LabAPI.Domain.Enums;

public sealed class EnSex : Enumeration<EnSex>
{
	public static readonly EnSex Male = new(0, "Male");
	public static readonly EnSex Female = new(1, "Female");
	private EnSex(int id, string name) : base(id, name)
	{
	}
}
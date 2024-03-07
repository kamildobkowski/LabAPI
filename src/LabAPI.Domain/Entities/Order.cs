using LabAPI.Domain.Common;
using LabAPI.Domain.ValueObjects;

namespace LabAPI.Domain.Entities;

public sealed class Order : BaseEntity<Guid>
{
	public PatientData PatientData { get; set; } = null!;
	public List<TestPanel> Tests { get; set; } = null!;
}
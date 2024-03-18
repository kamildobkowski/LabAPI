using LabAPI.Domain.Common;
using LabAPI.Domain.ValueObjects;

namespace LabAPI.Domain.Entities;

public sealed class Order : BaseEntity
{
	public string OrderNumber { get; set; } = string.Empty;
	public PatientData PatientData { get; set; } = null!;
	public List<string> Tests { get; set; } = [];
}
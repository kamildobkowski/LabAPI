using LabAPI.Domain.Common;
using LabAPI.Domain.ValueObjects;
using Newtonsoft.Json;

namespace LabAPI.Domain.Entities;

public sealed class Order : BaseEntity
{
	[JsonProperty("orderNumber", NullValueHandling = NullValueHandling.Ignore)]
	public string OrderNumber { get; set; } = null!;
	public PatientData PatientData { get; set; } = null!;
	public List<string> Tests { get; set; } = [];
}
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace LabAPI.Domain.Common;

public abstract class BaseEntity
{
	[JsonProperty("id")] public string Id { get; set; } = Guid.NewGuid().ToString();
	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public DateTime? ModifiedAt { get; set; } = DateTime.Now;
}
namespace LabAPI.Domain.Common;

public abstract class BaseEntity<T>
{
	public DateTime CreatedAt { get; set; } = DateTime.Now;
	public DateTime ModifiedAt { get; set; } = DateTime.Now;
}
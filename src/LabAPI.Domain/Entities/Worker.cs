namespace LabAPI.Domain.Entities;

public sealed class Worker : User
{
	public string? CollectionPointId { get; set; }
}
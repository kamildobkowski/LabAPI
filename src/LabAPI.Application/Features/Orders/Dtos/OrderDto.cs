using LabAPI.Domain.ValueObjects;

namespace LabAPI.Application.Features.Orders.Dtos;

public sealed record OrderDto
{
	public string OrderNumber { get; init; } = string.Empty;
	public string FullName { get; set; } = string.Empty;
	public string? Pesel { get; init; }
	public DateOnly DateOfBirth { get; init; }
	public string? Sex { get; init; }
	public string Status { get; set; } = string.Empty;
	public Address? Address { get; init; }
	public Dictionary<string, Dictionary<string, string>?> Results { get; init; } = null!;
}
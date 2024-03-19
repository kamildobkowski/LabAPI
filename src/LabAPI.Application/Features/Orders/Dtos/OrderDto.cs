using LabAPI.Domain.ValueObjects;

namespace LabAPI.Application.Features.Orders.Dtos;

public sealed record OrderDto
{
	public string OrderNumber { get; init; } = string.Empty;
	public string? Pesel { get; init; }
	public DateOnly DateOfBirth { get; init; }
	public string? Sex { get; init; }
	public Address? Address { get; init; }
	public List<string> Tests { get; init; } = [];
}
using LabAPI.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LabAPI.Application.Features.Orders.Dtos;

public sealed record OrderDto
{
	public string OrderNumber { get; init; } = string.Empty;
	public string FullName { get; init; } = string.Empty;
	public string? Pesel { get; init; }
	public DateOnly DateOfBirth { get; init; }
	public string? Sex { get; init; }
	public string Status { get; init; } = string.Empty;
	public Address? Address { get; init; }
	public List<TestsWithResultsDto> Tests { get; init; } = [];

	public sealed record TestsWithResultsDto
	{
		public string ShortName { get; init; } = string.Empty;
		public string Id { get; init; } = string.Empty;
		public string Name { get; init; } = string.Empty;
		public List<MarkerWithResultDto> Markers { get; set; } = [];
		public sealed record MarkerWithResultDto
		{
			public string Name { get; init; } = string.Empty;
			public string? Result { get; init; }
			public string? LowerNorm { get; init; }
			public string? HigherNorm { get; init; }
			public string? Unit { get; init; }
			public string? ShortName { get; init; }	
		}
	}
}
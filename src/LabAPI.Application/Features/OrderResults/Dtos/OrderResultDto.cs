namespace LabAPI.Application.Features.OrderResults.Dtos;

public sealed record OrderResultDto
{
	public string OrderNumber { get; init; } = null!;
	public Dictionary<string, Dictionary<string, string>> Results { get; init; } = null!;
}
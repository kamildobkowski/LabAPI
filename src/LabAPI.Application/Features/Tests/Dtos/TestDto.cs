using LabAPI.Application.Features.Orders.Dtos;

namespace LabAPI.Application.Dtos.Tests;

public sealed record TestDto(string ShortName, string Name, List<MarkerDto> Markers, string Id);
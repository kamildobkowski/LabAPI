using AutoMapper;
using LabAPI.Application.Dtos.Markers;
using LabAPI.Application.Dtos.Tests;
using LabAPI.Domain.Entities;
using LabAPI.Domain.ValueObjects;

namespace LabAPI.Application.MappingProfiles;

public class TestMappingProfile : Profile
{
	public TestMappingProfile()
	{
		CreateMap<CreateMarkerDto, Marker>();
		CreateMap<CreateTestDto, Test>()
			.ForMember(r => r.Markers, c => c.MapFrom(s => s.Markers));
	}
}
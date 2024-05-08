using AutoMapper;
using LabAPI.Application.Features.Orders.Dtos;
using LabAPI.Application.Features.Tests.Dtos;
using LabAPI.Domain.Entities;
using LabAPI.Domain.ValueObjects;

namespace LabAPI.Application.Features.Tests.MappingProfiles;

public class TestMappingProfile : Profile
{
	public TestMappingProfile()
	{
		CreateMap<CreateMarkerDto, Marker>();
		CreateMap<Marker, MarkerDto>();
		CreateMap<CreateTestDto, Test>()
			.ForMember(r => r.Markers, c => c.MapFrom(s => s.Markers));
		CreateMap<Test, TestDto>()
			.ForMember(r => r.Markers, c => c.MapFrom(s => s.Markers));
		CreateMap<UpdateTestDto, Test>()
			.ForMember(r => r.Markers, c => c.MapFrom(s => s.Markers));
		CreateMap<Test, TestNoMarkersDto>();
	}
}
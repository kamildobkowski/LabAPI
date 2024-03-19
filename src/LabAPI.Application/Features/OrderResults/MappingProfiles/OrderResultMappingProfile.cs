using AutoMapper;
using LabAPI.Application.Features.OrderResults.Dtos;
using LabAPI.Domain.Entities;

namespace LabAPI.Application.Features.OrderResults.MappingProfiles;

public sealed class OrderResultMappingProfile : Profile
{
	public OrderResultMappingProfile()
	{
		CreateMap<CreateOrderResultDto, OrderResult>();
	}
}
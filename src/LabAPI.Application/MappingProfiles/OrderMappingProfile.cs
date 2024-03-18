using AutoMapper;
using LabAPI.Application.Dtos.Orders;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Enums;
using LabAPI.Domain.ValueObjects;

namespace LabAPI.Application.MappingProfiles;

public sealed class OrderMappingProfile : Profile
{
	public OrderMappingProfile()
	{
		CreateMap<CreateOrderDto, Order>()
			.ForMember(r => r.PatientData,
				c => c.MapFrom(s =>
					new PatientData(s.Pesel, s.DateOfBirth, s.Sex!,
						new Address(s.AddressNumber, s.AddressStreet, s.AddressCity, s.AddressPostalCode))));
		CreateMap<Order, OrderDto>()
			.ForMember(r => r.Sex, c => c.MapFrom(s => s.PatientData.Sex))
			.ForMember(r => r.DateOfBirth, c => c.MapFrom(s => s.PatientData.DateOfBirth))
			.ForMember(r => r.Pesel, c => c.MapFrom(s => s.PatientData.Pesel))
			.ForMember(r => r.Address, c => c.MapFrom(s => s.PatientData.Address));
	}
}
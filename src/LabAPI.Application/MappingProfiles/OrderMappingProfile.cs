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
	}
}
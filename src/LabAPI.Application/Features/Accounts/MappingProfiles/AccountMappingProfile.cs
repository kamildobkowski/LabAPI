using AutoMapper;
using LabAPI.Application.Features.Accounts.Dtos;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Enums;

namespace LabAPI.Application.Features.Accounts.MappingProfiles;

public sealed class AccountMappingProfile : Profile
{
	public AccountMappingProfile()
	{
		CreateMap<RegisterCustomerDto, Customer>()
			.ForMember(r => r.Role, s=>s.MapFrom(c=>UserRole.Customer));
		CreateMap<RegisterWorkerDto, Worker>()
			.ForMember(r => r.Role, s => s.MapFrom(c => Enum.Parse<UserRole>(c.UserRole)));
	}
}
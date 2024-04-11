using LabAPI.Domain.Entities;

namespace LabAPI.Application.Common.Interfaces;

public interface IJwtService
{
	string GenerateToken(Customer customer);
	string GenerateToken(Worker worker);
}
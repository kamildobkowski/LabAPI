using LabAPI.Domain.Enums;

namespace LabAPI.Domain.Entities;

public sealed class Customer : User
{
	public override UserRole Role { get; set; } = UserRole.Customer;
	public string Pesel { get; set; } = null!;
}
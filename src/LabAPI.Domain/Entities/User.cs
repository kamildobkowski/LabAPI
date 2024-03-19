using LabAPI.Domain.Common;
using LabAPI.Domain.Enums;

namespace LabAPI.Domain.Entities;

public abstract class User : BaseEntity
{
	public string Email { get; set; } = null!;
	public string PasswordHash { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string Surname { get; set; } = null!;
	public virtual UserRole Role { get; set; }
}
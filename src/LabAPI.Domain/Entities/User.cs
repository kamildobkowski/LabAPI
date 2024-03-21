using System.Text;
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

	public static string GeneratePassword(int length = 16)
	{
		var random = new Random();
		var password = new StringBuilder();

		for (int i = 0; i < length; i++)
		{
			var randomChar = (char)random.Next(97, 123);
			if (random.Next(2) == 1)
			{
				randomChar = (char)(randomChar - 32);
			}
			password.Append(randomChar);
		}

		return password.ToString();
	}
}
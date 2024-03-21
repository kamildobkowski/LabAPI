using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LabAPI.Application.Common.Interfaces;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Enums;
using Microsoft.IdentityModel.Tokens;

namespace LabAPI.Infrastructure.Authentication;

public sealed class JwtService(AuthenticationSettings authenticationSettings) : IJwtService
{
	public string GenerateToken(Customer customer)
	{
		var claims = new List<Claim>
		{
			new(Claims.Id, customer.Id),
			new(Claims.FullName, $"{customer.Name} {customer.Surname}"),
			new(Claims.Role, $"{customer.Role}"),
			new(Claims.Email, $"{customer.Email}")
		};
		return GenerateToken(claims);
	}
	public string GenerateToken(Worker worker)
	{
		var claims = new List<Claim>
		{
			new(Claims.Id, worker.Id),
			new(Claims.FullName, $"{worker.Name} {worker.Surname}"),
			new(Claims.Role, $"{worker.Role}"),
			new(Claims.Email, $"{worker.Email}")
		};
		if (worker.Role == UserRole.CollectionPointWorker)
		{
			claims.Add(new Claim(Claims.CollectionPointId, worker.CollectionPointId!));
		}
		return GenerateToken(claims);
	}

	private string GenerateToken(IEnumerable<Claim> claims)
	{
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
		var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var date = DateTime.Now.AddDays(authenticationSettings.JwtExpireDays);

		var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,
			authenticationSettings.JwtIssuer, 
			claims, 
			expires: date, 
			signingCredentials: cred
		);

		var tokenHandler = new JwtSecurityTokenHandler();
		return tokenHandler.WriteToken(token);
	}
}
using System.Security.Claims;
using LabAPI.Domain.Enums;

namespace LabAPI.Infrastructure.Authentication.UserContext;

public interface IUserContextService
{
    ClaimsPrincipal? User { get; }
    UserRole UserRole { get; }
    string UserId { get; }
    string UserFullName { get; }
    string Email { get; }
    string? Pesel { get; }
}
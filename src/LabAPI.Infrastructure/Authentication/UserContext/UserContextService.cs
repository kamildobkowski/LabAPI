using System.Security.Claims;
using LabAPI.Domain.Enums;
using LabAPI.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace LabAPI.Infrastructure.Authentication.UserContext;

public sealed class UserContextService : IUserContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
    public UserRole UserRole
    {
        get
        {
            if (User is null)
                throw new UnauthorizedAccessException();
            var claim = User.Claims.FirstOrDefault(r => r.Type == Claims.Role)?.Value;
            return Enum.TryParse<UserRole>(claim, out var role) ? role : throw new UnauthorizedException();
        }
    }

    public string UserId
    {
        get
        {
            if (User is null)
                throw new UnauthorizedAccessException();
            var claim = User.Claims.FirstOrDefault(r => r.Type == Claims.Id)?.Value;
            return claim ?? throw new UnauthorizedAccessException();
        }
    }

    public string UserFullName
        => User?.Claims.FirstOrDefault(r => r.Type == Claims.FullName)?.Value!;

    public string Email
        => User?.Claims.FirstOrDefault(r => r.Type == Claims.Email)?.Value!;

    public string? Pesel
        => User?.Claims.FirstOrDefault(r => r.Type == Claims.Pesel)?.Value;
}
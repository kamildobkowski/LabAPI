using LabAPI.Domain.Entities;
using LabAPI.Domain.Enums;
using LabAPI.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace LabAPI.Infrastructure.Authorization.Policies;

public sealed class IsLabWorkerRequirement() : IAuthorizationRequirement
{
	
}

public sealed class IsLabWorkerRequirementHandler : AuthorizationHandler<IsLabWorkerRequirement>
{
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsLabWorkerRequirement requirement)
	{
		if(!context.User.Identity?.IsAuthenticated ?? true)
		{
			context.Fail();
			return Task.CompletedTask;
		}
		var role = context.User.FindFirst(r => r.Type == Claims.Role);
		if (!Enum.TryParse<UserRole>(role!.Value, ignoreCase: true, out var enRole))
		{
			context.Fail();
			return Task.CompletedTask;
		}
		if(enRole != UserRole.Customer && enRole != UserRole.Admin)
			context.Succeed(requirement);
		return Task.CompletedTask;
	}
}
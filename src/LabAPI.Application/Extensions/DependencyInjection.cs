using LabAPI.Application.Features.Tests.Commands;
using LabAPI.Application.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace LabAPI.Application.Extensions;

public static class DependencyInjection
{
	public static void AddApplication(this IServiceCollection services)
	{
		services.AddAutoMapper(typeof(TestMappingProfile));
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTestCommand).Assembly));
	}
}
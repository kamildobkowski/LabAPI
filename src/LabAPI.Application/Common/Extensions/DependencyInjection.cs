using FluentValidation;
using FluentValidation.AspNetCore;
using LabAPI.Application.Features.Tests.Commands;
using LabAPI.Application.Features.Tests.Dtos;
using LabAPI.Application.Features.Tests.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;

namespace LabAPI.Application.Common.Extensions;

public static class DependencyInjection
{
	public static void AddApplication(this IServiceCollection services)
	{
		services.AddAutoMapper(typeof(TestMappingProfile));
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTestCommand).Assembly));
		services.AddValidatorsFromAssemblyContaining<CreateTestDtoValidator>()
			.AddFluentValidationAutoValidation();
	}
}
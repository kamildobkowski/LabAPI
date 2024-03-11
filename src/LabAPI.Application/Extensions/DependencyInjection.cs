using Microsoft.Extensions.DependencyInjection;

namespace LabAPI.Application.Extensions;

public static class DependencyInjection
{
	public static void AddApplication(this IServiceCollection services)
	{
		//services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));
	}
}
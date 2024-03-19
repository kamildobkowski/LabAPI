using System.Configuration;
using AutoNumber;
using Azure.Storage.Blobs;
using LabAPI.Application.Features.OrderResults.Repository;
using LabAPI.Application.Features.Orders.Repository;
using LabAPI.Application.Features.Tests.Repository;
using LabAPI.Infrastructure.Persistence;
using LabAPI.Infrastructure.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LabAPI.Infrastructure.Extensions;

public static class DependencyInjection
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var cosmosClient =
			new CosmosClientBuilder(configuration.GetConnectionString("AzureCosmosDb"))
				.Build();
		services.AddSingleton(cosmosClient);
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddScoped<ITestRepository, TestRepository>();
		services.AddScoped<IOrderResultRepository, OrderResultRepository>();
		services.AddDbContext<CosmosDbContext>();
	}
}
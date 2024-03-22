using System.Text;
using Azure.Storage.Files.Shares;
using LabAPI.Application.Common.Interfaces;
using LabAPI.Application.Features.Accounts.Repository;
using LabAPI.Application.Features.Orders.Repository;
using LabAPI.Application.Features.Tests.Repository;
using LabAPI.Domain.Entities;
using LabAPI.Infrastructure.Authentication;
using LabAPI.Infrastructure.Authorization.Policies;
using LabAPI.Infrastructure.Persistence;
using LabAPI.Infrastructure.Repositories;
using LabAPI.Infrastructure.Services.Pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LabAPI.Infrastructure.Extensions;

public static class DependencyInjection
{
	public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddRepositories(configuration);
		services.AddJwtAuthentication(configuration);
		services.AddCustomAuthorization(configuration);
		services.AddSingleton<IPdfFileRepository, PdfFileRepository>();
		services.AddScoped<IPdfService, PdfService>();
	}

	private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
	{
		var cosmosClient =
			new CosmosClientBuilder(configuration.GetConnectionString("AzureCosmosDb"))
				.Build();
		services.AddSingleton(cosmosClient);
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddScoped<ITestRepository, TestRepository>();
		services.AddScoped<ICustomerRepository, CustomerRepository>();
		services.AddScoped<IWorkerRepository, WorkerRepository>();
		services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
	}

	private static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<IJwtService, JwtService>();
		var authenticationSettings = new AuthenticationSettings();
		configuration.GetSection("Authentication").Bind(authenticationSettings);

		services.AddAuthentication(option =>
		{
			option.DefaultAuthenticateScheme = "Bearer";
			option.DefaultScheme = "Bearer";
			option.DefaultChallengeScheme = "Bearer";
		}).AddJwtBearer(cfg =>
		{
			cfg.RequireHttpsMetadata = false;
			cfg.SaveToken = true;
			cfg.TokenValidationParameters = new TokenValidationParameters
			{
				ValidIssuer = authenticationSettings.JwtIssuer,
				ValidAudience = authenticationSettings.JwtIssuer,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
			};
		});
		services.AddSingleton(authenticationSettings);
	}

	private static void AddCustomAuthorization(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddScoped<IAuthorizationHandler, IsLabWorkerRequirementHandler>();
		services.AddAuthorizationBuilder()
            .AddPolicy("IsLabWorker", b => b.AddRequirements(new IsLabWorkerRequirement()));
	}
}
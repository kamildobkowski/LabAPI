using LabAPI.Application.Features.Tests.Repository;
using LabAPI.Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace LabAPI.Infrastructure.Repositories;

internal sealed class TestRepository(CosmosClient cosmosClient, ILogger<TestRepository> logger) 
	: GenericRepository<Test>(cosmosClient, logger), ITestRepository
{
	public async Task CreateAsync(Test entity)
	{
		await base.CreateAsync(entity, entity.ShortName);
	}

	public async Task UpdateAsync(Test entity)
	{
		await base.UpdateAsync(entity, entity.ShortName);
	}

	public async Task DeleteAsync(Test entity)
	{
		await base.DeleteAsync(entity, entity.ShortName);
	}
}
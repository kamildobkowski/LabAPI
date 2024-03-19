using LabAPI.Application.Interfaces;
using LabAPI.Domain.Entities;
using LabAPI.Infrastructure.Persistence;
using Microsoft.Azure.Cosmos;

namespace LabAPI.Infrastructure.Repositories;

internal sealed class TestRepository(CosmosClient cosmosClient, CosmosDbContext dbContext) 
	: GenericRepository<Test>(cosmosClient, dbContext), ITestRepository
{
	public async Task Create(Test entity)
	{
		await base.CreateAsync(entity);
	}

	public async Task Update(Test entity)
	{
		await base.UpdateAsync(entity);
	}

	public async Task Delete(Test entity)
	{
		await base.DeleteAsync(entity);
	}
}
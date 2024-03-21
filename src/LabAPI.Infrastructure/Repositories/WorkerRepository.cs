using LabAPI.Application.Features.Accounts.Repository;
using LabAPI.Domain.Entities;
using LabAPI.Infrastructure.Persistence;
using Microsoft.Azure.Cosmos;

namespace LabAPI.Infrastructure.Repositories;

internal class WorkerRepository(CosmosClient cosmosClient, CosmosDbContext dbContext) 
	: GenericRepository<Worker>(cosmosClient, dbContext), IWorkerRepository 
{
	public async Task Create(Worker entity)
	{
		await base.CreateAsync(entity, entity.Id);
	}

	public async Task Update(Worker entity)
	{
		await base.UpdateAsync(entity);
	}

	public async Task Delete(Worker entity)
	{
		await base.DeleteAsync(entity, entity.Id);
	}
}
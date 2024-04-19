using LabAPI.Domain.Entities;
using LabAPI.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace LabAPI.Infrastructure.Repositories;

internal class WorkerRepository(CosmosClient cosmosClient, ILogger<WorkerRepository> logger) 
	: GenericRepository<Worker>(cosmosClient, logger), IWorkerRepository 
{
	public async Task CreateAsync(Worker entity)
	{
		await base.CreateAsync(entity, entity.Id);
	}

	public async Task UpdateAsync(Worker entity)
	{
		await base.UpdateAsync(entity, entity.Id);
	}

	public async Task DeleteAsync(Worker entity)
	{
		await base.DeleteAsync(entity, entity.Id);
	}
}
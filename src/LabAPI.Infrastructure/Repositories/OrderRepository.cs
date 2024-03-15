using LabAPI.Application.Interfaces;
using LabAPI.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace LabAPI.Infrastructure.Repositories;

internal sealed class OrderRepository(CosmosClient cosmosClient) 
	:  GenericRepository<Order>(cosmosClient), IOrderRepository
{
	public async Task CreateAsync(Order entity)
	{
		await base.CreateAsync(entity);
	}

	public async Task UpdateAsync(Order entity)
	{
		await base.UpdateAsync(entity);
	}

	public async Task DeleteAsync(Order entity)
	{
		await base.DeleteAsync(entity, entity.Id);
	}
}
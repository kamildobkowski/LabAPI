using LabAPI.Application.Interfaces;
using LabAPI.Domain.Entities;
using LabAPI.Infrastructure.Persistence;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;

namespace LabAPI.Infrastructure.Repositories;

internal sealed class OrderRepository(CosmosDbContext dbContext, CosmosClient cosmosClient) 
	:  GenericRepository<Order>(cosmosClient, dbContext), IOrderRepository
{
	private readonly CosmosDbContext _dbContext = dbContext;
	private readonly Container _container = cosmosClient.GetContainer("LabApi", nameof(Order) + "s");

	public async Task<string> CreateWithNewIdAsync(Order entity)
	{
		var latestOrderQuery = _container.GetItemLinqQueryable<Order>()
			.OrderByDescending(r => r.OrderNumber)
			.ToFeedIterator();
		var latestOrder = (await latestOrderQuery.ReadNextAsync())
							.Resource
							.FirstOrDefault();
		var s = "1000000000";
		if (latestOrder is not null)
			s = (int.Parse(latestOrder.OrderNumber)+1).ToString();
		entity.OrderNumber = s;
		await Create(entity);
		return s;
	}

	public async Task Create(Order entity)
	{
		await base.CreateAsync(entity);
	}

	public async Task Update(Order entity)
	{
		await base.UpdateAsync(entity);
	}

	public async Task Delete(Order entity)
	{
		await base.DeleteAsync(entity, entity.OrderNumber);
	}
}
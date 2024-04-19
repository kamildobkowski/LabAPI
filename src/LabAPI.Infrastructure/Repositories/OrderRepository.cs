using LabAPI.Domain.Common;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Repositories;
using LabAPI.Infrastructure.Persistence;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LabAPI.Infrastructure.Repositories;

internal sealed class OrderRepository(CosmosClient cosmosClient, ILogger<OrderRepository> logger) 
	:  GenericRepository<Order>(cosmosClient, logger), IOrderRepository
{
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
		entity.Id = s;
		await CreateAsync(entity);
		return s;
	}

	public async Task CreateAsync(Order entity)
	{
		await base.CreateAsync(entity, entity.OrderNumber);
	}

	public async Task UpdateAsync(Order entity)
	{
		await base.UpdateAsync(entity, entity.OrderNumber);
	}

	public async Task DeleteAsync(Order entity)
	{
		await base.DeleteAsync(entity, entity.OrderNumber);
	}

	public async Task<PagedList<Order>> GetPageAsync(int page, int pageSize, string? filter, string? orderBy, bool sortOrder)
	{
		return await base.GetPageAsync(page, pageSize, 
			r => 
				filter==null ||
				r.OrderNumber==filter ||
				r.PatientData.Surname.Contains(filter), 
			orderBy, sortOrder);
	}
}
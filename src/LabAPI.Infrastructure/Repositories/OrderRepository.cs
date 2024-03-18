using LabAPI.Application.Interfaces;
using LabAPI.Domain.Entities;
using LabAPI.Infrastructure.Persistence;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace LabAPI.Infrastructure.Repositories;

internal sealed class OrderRepository(CosmosDbContext dbContext) 
	:  GenericRepository<Order>(dbContext), IOrderRepository
{
	private readonly CosmosDbContext _dbContext = dbContext;
	

	public async Task<string> CreateWithNewIdAsync(Order entity)
	{
		var latestOrder = (await _dbContext.Orders.OrderByDescending(r => r.OrderNumber)
			.FirstOrDefaultAsync());
		var s = "1000000000";
		if (latestOrder is not null)
			s = (int.Parse(latestOrder.OrderNumber)+1).ToString();
		entity.OrderNumber = s;
		base.Create(entity);
		return s;
	}
}
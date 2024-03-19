using System.Linq.Expressions;
using LabAPI.Application.Features.OrderResults.Repository;
using LabAPI.Domain.Common;
using LabAPI.Domain.Entities;
using LabAPI.Infrastructure.Persistence;
using Microsoft.Azure.Cosmos;

namespace LabAPI.Infrastructure.Repositories;

internal class OrderResultRepository(CosmosClient cosmosClient, CosmosDbContext dbContext) : GenericRepository<OrderResult>(cosmosClient, dbContext), IOrderResultRepository
{
	public async Task Create(OrderResult entity)
	{
		await base.CreateAsync(entity);
	}

	public async Task Update(OrderResult entity)
	{
		await base.UpdateAsync(entity);
	}

	public async Task Delete(OrderResult entity)
	{
		await base.DeleteAsync(entity, entity.OrderNumber);
	}
}
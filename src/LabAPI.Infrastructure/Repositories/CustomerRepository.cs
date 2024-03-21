using LabAPI.Application.Features.Accounts.Repository;
using LabAPI.Domain.Entities;
using LabAPI.Infrastructure.Persistence;
using Microsoft.Azure.Cosmos;

namespace LabAPI.Infrastructure.Repositories;

internal sealed class CustomerRepository(CosmosClient cosmosClient, CosmosDbContext dbContext) 
	: GenericRepository<Customer>(cosmosClient, dbContext), ICustomerRepository
{
	public async Task Create(Customer entity)
	{
		await base.CreateAsync(entity, entity.Id);
	}

	public async Task Update(Customer entity)
	{
		await base.UpdateAsync(entity);
	}

	public async Task Delete(Customer entity)
	{
		await base.DeleteAsync(entity, entity.Id);
	}
}
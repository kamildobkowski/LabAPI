using LabAPI.Application.Features.Accounts.Repository;
using LabAPI.Domain.Entities;
using LabAPI.Infrastructure.Persistence;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace LabAPI.Infrastructure.Repositories;

internal sealed class CustomerRepository(CosmosClient cosmosClient, ILogger<CustomerRepository> logger) 
	: GenericRepository<Customer>(cosmosClient, logger), ICustomerRepository
{
	public async Task CreateAsync(Customer entity)
	{
		await base.CreateAsync(entity, entity.Id);
	}

	public async Task UpdateAsync(Customer entity)
	{
		await base.UpdateAsync(entity);
	}

	public async Task DeleteAsync(Customer entity)
	{
		await base.DeleteAsync(entity, entity.Id);
	}
}
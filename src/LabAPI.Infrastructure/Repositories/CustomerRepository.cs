using LabAPI.Domain.Entities;
using LabAPI.Domain.Repositories;
using LabAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace LabAPI.Infrastructure.Repositories;

internal sealed class CustomerRepository(CosmosClient cosmosClient, ILogger<CustomerRepository> logger, 
	IMediator mediator, LabDbContext dbContext) 
	: GenericRepository<Customer>(cosmosClient, logger, mediator, dbContext), ICustomerRepository
{
	
}
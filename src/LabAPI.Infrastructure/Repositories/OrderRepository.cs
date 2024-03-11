using System.Linq.Expressions;
using LabAPI.Application.Interfaces;
using LabAPI.Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace LabAPI.Infrastructure.Repositories;

internal sealed class OrderRepository(CosmosClient cosmosClient) 
	:  GenericRepository<Order>(cosmosClient), IOrderRepository
{
	
}
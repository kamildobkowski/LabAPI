using LabAPI.Domain.Entities;
using LabAPI.Domain.Repositories;
using LabAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace LabAPI.Infrastructure.Repositories;

internal class WorkerRepository(CosmosClient cosmosClient, ILogger<WorkerRepository> logger, 
	IMediator mediator, LabDbContext dbContext) 
	: GenericRepository<Worker>(cosmosClient, logger, mediator, dbContext), IWorkerRepository 
{
	
}
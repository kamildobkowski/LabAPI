using LabAPI.Domain.Common;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Repositories;
using LabAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LabAPI.Infrastructure.Repositories;

internal class WorkerRepository(CosmosClient cosmosClient, ILogger<WorkerRepository> logger, 
	IMediator mediator, LabDbContext dbContext) 
	: GenericRepository<Worker>(cosmosClient, logger, mediator, dbContext), IWorkerRepository 
{
	public async Task<PagedList<Worker>> GetPageAsync(int page, int pageSize, string? filter, string? orderBy, bool sortOrder)
	{
		var filterList = filter?.Split(' ');
		var pagedList =  await base.GetPageAsync(page, pageSize, 
			r=>true, 
			orderBy, sortOrder);
		pagedList.List = pagedList.List
			.AsParallel()
			.Where(r => filter == null || 
			            filter.IsNullOrEmpty() ||
			            filterList == null ||
			            r.Name.Contains(filter) ||
			            r.Surname.Contains(filter) ||
			            $"{r.Name} {r.Surname}".Contains(filter) ||
			            r.Email.Contains(filter))
			.ToList();
		return pagedList;
	}
}
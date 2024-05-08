using LabAPI.Domain.Common;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Repositories;
using LabAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LabAPI.Infrastructure.Repositories;

internal sealed class TestRepository(
	CosmosClient cosmosClient,
	ILogger<TestRepository> logger,
	IMediator mediator,
	LabDbContext dbContext)
	: GenericRepository<Test>(cosmosClient, logger, mediator, dbContext), ITestRepository
{
	public async Task<PagedList<Test>> GetPageAsync(int page, int pageSize, string? filter, string? orderBy, bool sortOrder)
	{
		filter = filter?.ToLower();
		return await base.GetPageAsync(
			page,
			pageSize,
			r =>
				filter==null || 
				filter.IsNullOrEmpty() ||
				r.Name.ToLower().Contains(filter) || r.ShortName.ToLower().Contains(filter),
			orderBy, sortOrder);
		}
}
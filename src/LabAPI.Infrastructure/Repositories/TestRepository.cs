using System.Linq.Expressions;
using LabAPI.Domain.Common;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Repositories;
using LabAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
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
	public override async Task<Test?> GetAsync(Expression<Func<Test, bool>> lambda)
	{
		var entity = await base.GetAsync(lambda);
		if (entity is null)
			return null;
		var entry = dbContext.Entry(entity);
		if (entry.State == EntityState.Detached)
		{
			dbContext.Set<Test>().Attach(entity);
		}
		return entity;
	}

	public async Task<PagedList<Test>> GetPageAsync(int page, int pageSize, string? filter, string? orderBy, bool sortOrder)
	{
		return await base.GetPageAsync(
			page,
			pageSize,
			r =>
				filter==null || 
				filter.IsNullOrEmpty() ||
				r.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase) || 
				r.ShortName.Contains(filter, StringComparison.CurrentCultureIgnoreCase),
			orderBy, sortOrder);
		}
}
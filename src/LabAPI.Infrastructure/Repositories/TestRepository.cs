using LabAPI.Domain.Common;
using LabAPI.Domain.Entities;
using LabAPI.Domain.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LabAPI.Infrastructure.Repositories;

internal sealed class TestRepository(CosmosClient cosmosClient, ILogger<TestRepository> logger) 
	: GenericRepository<Test>(cosmosClient, logger), ITestRepository
{
	public async Task CreateAsync(Test entity)
	{
		await base.CreateAsync(entity, entity.ShortName);
	}

	public async Task UpdateAsync(Test entity)
	{
		await base.UpdateAsync(entity, entity.ShortName);
	}

	public async Task DeleteAsync(Test entity)
	{
		await base.DeleteAsync(entity, entity.ShortName);
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
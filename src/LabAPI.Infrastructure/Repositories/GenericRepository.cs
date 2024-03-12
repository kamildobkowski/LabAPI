using System.Linq.Expressions;
using LabAPI.Application.Interfaces;
using LabAPI.Domain.Common;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace LabAPI.Infrastructure.Repositories;

internal abstract class GenericRepository<T> (CosmosClient cosmosClient) where T : BaseEntity
{
	private readonly Container _container = cosmosClient.GetContainer("LabApi", typeof(T).Name + "s");
	public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> lambda)
	{
		using var q = _container.GetItemLinqQueryable<T>().Where(lambda).ToFeedIterator();
		return (await q.ReadNextAsync()).Resource.FirstOrDefault();
	}

	public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> lambda = default!)
	{
		using var q = _container.GetItemLinqQueryable<T>().Where(lambda).ToFeedIterator();
		{
			var list = new List<T>();
			while (q.HasMoreResults)
			{
				list.AddRange(await q.ReadNextAsync());
			}
			return list;
		}
	}

	public virtual async Task CreateAsync(T entity, string? partitionKey = null)
		=> await _container.CreateItemAsync(entity);


	public virtual async Task UpdateAsync(T entity, string? partitionKey = null)
		=> await _container.UpsertItemAsync(entity);

	public virtual Task DeleteAsync(T entity, string? partitionKey = null)
		=> _container.DeleteItemAsync<T>(entity.Id, new PartitionKey(partitionKey));
}
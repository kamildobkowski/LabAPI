using System.Globalization;
using System.Linq.Expressions;
using LabAPI.Application.Common.Interfaces;
using LabAPI.Domain.Common;
using LabAPI.Infrastructure.Persistence;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;

namespace LabAPI.Infrastructure.Repositories;

public abstract class GenericRepository<T> (CosmosClient cosmosClient, CosmosDbContext dbContext) 
	: IPagination<T> where T : BaseEntity
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

	public virtual async Task CreateAsync(T entity, string? partitionKey=null)
		=> await _container.CreateItemAsync(entity);


	public virtual async Task UpdateAsync(T entity)
	{
		entity.ModifiedAt=DateTime.UtcNow;
		await _container.UpsertItemAsync(entity);
	}

	public virtual Task DeleteAsync(T entity, string? partitionKey = null)
		=> _container.DeleteItemAsync<T>(entity.Id.ToString(), new PartitionKey(partitionKey));
	
	protected virtual IQueryable<T> PaginationQuery(int page, int pageSize, Expression<Func<T, object>> orderBy,
		Expression<Func<T, bool>> filter, bool asc = true)
		=> !asc
			? _container.GetItemLinqQueryable<T>()
				.Where(filter)
				.OrderBy(orderBy)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
			: _container.GetItemLinqQueryable<T>()
				.Where(filter)
				.OrderByDescending(orderBy)
				.Skip((page - 1) * pageSize)
				.Take(pageSize);

	protected virtual IQueryable<T> PaginationQuery(int page, int pageSize, string? filterBy, string? filter,
		string? orderBy, bool sortOrder = true)
	{
		Expression<Func<T, object>> orderByLambda;
		if (string.IsNullOrEmpty(orderBy))
		{
			orderByLambda = (entity) => entity.Id;
		}
		else
		{
			var property = typeof(T).GetProperty(orderBy);
			if (property is null)
			{
				orderByLambda = (entity) => entity.Id;
			}
			else
			{
				var parameter = Expression.Parameter(typeof(T));
				var propertyAccess = Expression.Property(parameter, property);
				var convertExpression = Expression.Convert(propertyAccess, typeof(object));
				orderByLambda = Expression.Lambda<Func<T, object>>(convertExpression, parameter);
			}
		}

		Expression<Func<T, bool>> filterLambda;
		if (string.IsNullOrEmpty(filterBy) || string.IsNullOrEmpty(filter))
			filterLambda = (entity) => true;
		else
		{
			var filterProperty = typeof(T).GetProperty(filterBy);
			if (filterProperty is null)
			{
				filterLambda = entity => true;
			}
			else
			{
				var parameter = Expression.Parameter(typeof(T));
				var propertyAccess = Expression.Property(parameter, filterProperty);
				var valueExpression = Expression.Constant(filter.ToLower());
				var toLowerMethod = typeof(string).GetMethod("ToLower", System.Type.EmptyTypes);
				var propertyToLower = Expression.Call(propertyAccess, toLowerMethod!);
				var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
				var containsExpression = Expression.Call(propertyToLower, containsMethod!, valueExpression);
				filterLambda = Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
			}
		}

		return PaginationQuery
			(page, pageSize, orderByLambda, filterLambda, sortOrder);
	}
	public async Task<PagedList<T>> GetPageAsync(int page, int pageSize, string? filterBy, string? filter, string? orderBy ,bool sortOrder = true)
	{
		using var q = PaginationQuery(page, pageSize, filterBy, filter, orderBy, sortOrder).ToFeedIterator();
		{
			var list = new List<T>();
			while (q.HasMoreResults)
			{
				list.AddRange(await q.ReadNextAsync());
			}
			return new PagedList<T>(list, page, pageSize, list.Count);
		}
		
	}

	public virtual async Task SaveChangesAsync()
		=> await dbContext.SaveChangesAsync();
}
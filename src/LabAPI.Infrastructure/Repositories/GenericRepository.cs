using System.Globalization;
using System.Linq.Expressions;
using LabAPI.Application.Common.Interfaces;
using LabAPI.Domain.Common;
using LabAPI.Domain.Exceptions;
using LabAPI.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LabAPI.Infrastructure.Repositories;

public abstract class GenericRepository<T>(CosmosClient cosmosClient, ILogger<GenericRepository<T>> logger) where T : BaseEntity
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
	{
		try
		{
			await _container.CreateItemAsync(entity);
			logger.LogInformation($"Created entity {nameof(T)}");
		}
		catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.Conflict)
		{
			logger.LogError(e.Message, e);
			throw new BadHttpRequestException("Entity already exists");
		}
		catch(CosmosException e)
		{
			logger.LogError(e.Message, e);
			throw new BadHttpRequestException("Error while creating entity");
		}
		
	}
	


	public virtual async Task UpdateAsync(T entity, string? partitionKey = null)
	{
		try
		{
			entity.ModifiedAt=DateTime.UtcNow;
			var i = await _container.UpsertItemAsync(entity);
			logger.LogInformation($"Updated entity {nameof(T)} with id {entity.Id}");
		}
		catch(CosmosException e)
		{
			logger.LogError(e.Message, e);
			throw new BadHttpRequestException("Error while updating entity");
		}
	}

	public virtual Task DeleteAsync(T entity, string partitionKey)
	{
		try
		{
			return _container.DeleteItemAsync<T>(entity.Id, new PartitionKey(partitionKey));
		}
		catch (CosmosException e) when (e.StatusCode == System.Net.HttpStatusCode.NotFound)
		{
			throw new NotFoundException(e.Message);
		}
	}
	
	protected virtual IQueryable<T> PaginationQuery(int page, int pageSize, Expression<Func<T, object>> orderBy,
		Expression<Func<T, bool>> filter, bool asc = true)
		=> asc
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

	protected virtual IQueryable<T> PaginationQuery(int page, int pageSize, Expression<Func<T, bool>> filterByLambda,
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

		return PaginationQuery
			(page, pageSize, orderByLambda, filterByLambda, sortOrder);
	}
	public async Task<PagedList<T>> GetPageAsync(int page, int pageSize, Expression<Func<T, bool>> filterByLambda, string? orderBy ,bool sortOrder = true)
	{
		var allItemsCount = _container.GetItemLinqQueryable<T>().Count();
		try
		{
			using var q = PaginationQuery(page, pageSize, filterByLambda, orderBy, sortOrder).ToFeedIterator();
			{
				var list = new List<T>();
				while (q.HasMoreResults)
				{
					list.AddRange(await q.ReadNextAsync());
				}

				logger.LogInformation($"Get Page of {nameof(T)}");
				return new PagedList<T>(list, page, pageSize, list.Count, allItemsCount);
			}
		}
		catch (Exception e)
		{
			logger.LogError("e.Message", [e]);
			throw new BadHttpRequestException("Error while getting page");
		}
		
		
	}
}
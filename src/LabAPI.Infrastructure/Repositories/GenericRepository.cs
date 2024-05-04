using System.Globalization;
using System.Linq.Expressions;
using LabAPI.Application.Common.Interfaces;
using LabAPI.Domain.Common;
using LabAPI.Domain.Exceptions;
using LabAPI.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LabAPI.Infrastructure.Repositories;

public abstract class GenericRepository<T>(CosmosClient cosmosClient, ILogger<GenericRepository<T>> logger, 
	IMediator mediator, LabDbContext dbContext) where T : Entity
{

	private readonly Container _container = cosmosClient.GetContainer("LabApi", typeof(T).Name + "s");
	// public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> lambda) 
	// {
	// 	using var q = _container.GetItemLinqQueryable<T>().Where(lambda).ToFeedIterator();
	// 	return (await q.ReadNextAsync()).Resource.FirstOrDefault();
	// }

	public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> lambda)
	{
		using var q = _container.GetItemLinqQueryable<T>().Where(lambda).ToFeedIterator();
		var entity = (await q.ReadNextAsync()).Resource.FirstOrDefault();
		AddTracking(entity);
		return entity;
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

	public virtual void CreateAsync(T entity)
		=> dbContext.Set<T>().Add(entity);

	public virtual void UpdateAsync(T entity)
	{
		entity.ModifiedAt=DateTime.UtcNow;
		dbContext.Set<T>().Update(entity);
		logger.LogInformation($"Updated entity {nameof(T)} with id {entity.Id}");
	}

	public virtual void DeleteAsync(T entity)
		=> dbContext.Set<T>().Remove(entity);
	
	
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
				AddTracking(list);
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

	public Task SaveChangesAsync()
		=> dbContext.SaveChangesAsync();
	

	private void AddTracking(T? entity)
	{
		if (entity is null)
			return;
		var entry = dbContext.Entry(entity);
		if (entry.State == EntityState.Detached)
			dbContext.Set<T>().Attach(entity);
	}

	private void AddTracking(IEnumerable<T> entities)
	{
		foreach (var i in entities)
		{
			var entry = dbContext.Entry(i);
			if(entry.State == EntityState.Detached)
				dbContext.Set<T>().Attach(i);
		}
	}
}
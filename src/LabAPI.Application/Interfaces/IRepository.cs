using System.Linq.Expressions;

namespace LabAPI.Application.Interfaces;

public interface IRepository<T> where T : class
{
	public Task<T?> GetAsync(Expression<Func<T, bool>> lambda);
	public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> lambda = null!);
	public Task CreateAsync(T entity);
	public Task UpdateAsync(T entity);
	public Task DeleteAsync(T entity);

}
using System.Linq.Expressions;

namespace LabAPI.Application.Interfaces;

public interface IRepository<T> where T : class
{
	public Task<T?> GetAsync(Expression<Func<T, bool>> lambda);
	public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> lambda = null!);
	public Task Create(T entity);
	public Task Update(T entity);
	public Task Delete(T entity);
	public Task SaveChangesAsync();

}
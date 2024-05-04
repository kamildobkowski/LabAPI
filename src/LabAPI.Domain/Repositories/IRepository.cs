using System.Linq.Expressions;

namespace LabAPI.Domain.Repositories;

public interface IRepository<T> where T : class
{
	public Task<T?> GetAsync(Expression<Func<T, bool>> lambda);
	public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> lambda);
	public void CreateAsync(T entity);
	public void UpdateAsync(T entity);
	public void DeleteAsync(T entity);
	public Task SaveChangesAsync();

}
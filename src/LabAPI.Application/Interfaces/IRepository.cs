using System.Linq.Expressions;

namespace LabAPI.Application.Interfaces;

public interface IRepository<T> where T : class
{
	public Task<T?> GetAsync(Expression<Func<T, bool>> lambda);
	public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> lambda = null!);
	public void Create(T entity);
	public void Update(T entity);
	public void Delete(T entity);
	public Task SaveChangesAsync();

}
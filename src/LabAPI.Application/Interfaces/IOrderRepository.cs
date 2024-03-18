using LabAPI.Domain.Entities;

namespace LabAPI.Application.Interfaces;

public interface IOrderRepository : IRepository<Order>, IPagination<Order>
{
	public Task<string> CreateWithNewIdAsync(Order entity);
}
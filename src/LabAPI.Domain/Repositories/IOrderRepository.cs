using LabAPI.Domain.Common;
using LabAPI.Domain.Entities;

namespace LabAPI.Domain.Repositories;

public interface IOrderRepository : IRepository<Order>, IPagination<Order>
{
	public Task<string> CreateWithNewIdAsync(Order entity);
}
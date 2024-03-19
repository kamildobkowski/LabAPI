using LabAPI.Application.Common.Interfaces;
using LabAPI.Domain.Entities;

namespace LabAPI.Application.Features.Orders.Repository;

public interface IOrderRepository : IRepository<Order>, IPagination<Order>
{
	public Task<string> CreateWithNewIdAsync(Order entity);
}
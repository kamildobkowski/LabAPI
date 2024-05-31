using LabAPI.Domain.Common;
using LabAPI.Domain.Entities;

namespace LabAPI.Domain.Repositories;

public interface IOrderRepository : IRepository<Order>, IPagination<Order>
{
	Task<string> CreateWithNewIdAsync(Order entity);

	Task<PagedList<Order>> GetPageAsync(int page, int pageSize, string? filter, string? orderBy,
		bool sortOrder, List<string>? statuses);
}
using LabAPI.Domain.Common;

namespace LabAPI.Application.Common.Interfaces;

public interface IPagination<T> where T : class
{
	Task<PagedList<T>> GetPageAsync(int page, int pageSize, string? filterBy, string? filter, string? orderBy, bool sortOrder);
}
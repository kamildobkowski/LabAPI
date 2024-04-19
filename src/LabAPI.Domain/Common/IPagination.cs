namespace LabAPI.Domain.Common;

public interface IPagination<T> where T : class
{
	Task<PagedList<T>> GetPageAsync(int page, int pageSize, string? filter, string? orderBy, bool sortOrder);
}
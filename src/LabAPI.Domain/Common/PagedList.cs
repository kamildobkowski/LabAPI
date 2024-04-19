using System.Collections;

namespace LabAPI.Domain.Common;

public class PagedList<T>
{
	public List<T> List { get; set; }
	public int Page { get; set; }
	public int PageSize { get; set; }
	public int PageCount { get; set; }
	public int Count { get; set; }
	public int AllItemsCount { get; set; }
	public bool HasNext { get; set; }
	public bool HasPrevious { get; set; }

	public PagedList(List<T> list, int page, int pageSize, int count, int allItemsCount)
	{
		List = list;
		Page = page;
		PageSize = pageSize;
		Count = count;
		AllItemsCount = allItemsCount;
		HasNext = page * pageSize < count;
		HasPrevious = page > 1;
		PageCount = (int)Math.Ceiling(allItemsCount/(double)pageSize);
	}
}
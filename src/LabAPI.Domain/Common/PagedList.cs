using System.Collections;

namespace LabAPI.Domain.Common;

public class PagedList<T> : IEnumerable<T>
{
	public List<T> List { get; set; }
	public int Page { get; set; }
	public int PageSize { get; set; }
	public int Count { get; set; }
	public bool HasNext { get; set; }
	public bool HasPrevious { get; set; }

	public PagedList(List<T> list, int page, int pageSize, int count)
	{
		List = list;
		Page = page;
		PageSize = pageSize;
		Count = count;
		HasNext = page * pageSize < count;
		HasPrevious = page > 1;
	}
	public IEnumerator<T> GetEnumerator()
	{
		return List.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
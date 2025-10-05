namespace CafeDotNet.Core.Helpers;

public class PaginatedList<T> : List<T>
{
    public const int PageCount = 5;

    public int PageIndex { get; private set; }
    public int TotalPages { get; private set; }

    public IEnumerable<int> Pages
    {
        get
        {
            var start = Math.Max(1, PageIndex - 2);
            var end = Math.Min(TotalPages, PageIndex + 2);
           
            return Enumerable.Range(start, end - start + 1);
        }
    }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }

    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex)
    {
        var count = source.Count();
        var items = source.Skip((pageIndex - 1) * PageCount).Take(PageCount).ToList();
        
        return new PaginatedList<T>(items, count, pageIndex, PageCount);
    }

    //public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex)
    //{
    //    var count = source.Count(); // com EF: await source.CountAsync();
    //    var items = source.Skip((pageIndex - 1) * PageCount).Take(PageCount).ToList(); // com EF: await source.Skip(...).Take(...).ToListAsync();

    //    return new PaginatedList<T>(items, count, pageIndex, PageCount);
    //}
}
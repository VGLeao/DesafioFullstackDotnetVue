using DedtechChallenge.Utils.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DedtechChallenge.Utils
{
    public class PaginatedList<T> : IPaginatedList<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage
        {
            get { return PageIndex > 0; }
        }
        public bool HasNextPage
        {
            get { return PageIndex + 1 < TotalPages; }
        }
        public List<T> Results { get; set; } = new List<T>();
        public PaginatedList() { }

        public static async Task<PaginatedList<T>> ToPagedListAsync(IQueryable<T> source, int pageIndex = 0, int pageSize = 10)
        {
            PaginatedList<T> paginatedList = new PaginatedList<T>();

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            int total = source.Count();
            paginatedList.TotalCount = total;
            paginatedList.TotalPages = total / pageSize;
            paginatedList.PageSize = pageSize;
            paginatedList.PageIndex = pageIndex;

            if (total % pageSize > 0)
            {
                paginatedList.TotalPages++;
            }

            paginatedList.Results = await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

            return paginatedList;
        }
    }
}

namespace DedtechChallenge.Utils.Interfaces
{
    public interface IPaginatedList<T>
    {
        int PageIndex { get; }

        int PageSize { get; }

        int TotalCount { get; }

        int TotalPages { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }

        List<T> Results { get; }
    }
}

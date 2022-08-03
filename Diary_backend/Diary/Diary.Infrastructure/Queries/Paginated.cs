using System.Collections.Generic;

namespace Diary.Infrastructure.Queries
{
    public sealed record Pagination(int PageIndex = 0, int PageSize = 20)
    {
        public int Skip()
        {
            return PageIndex * PageSize;
        }

        public int Take()
        {
            return PageSize;
        }
    }

    public sealed record PaginatedViewModel<T>(Pagination PaginatedQuery, int TotalSize, IEnumerable<T> Data) : EnumerableViewModel<T>(Data)
    {
    }
}

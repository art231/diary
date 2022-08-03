using System.Collections.Generic;

namespace Diary.Infrastructure.Queries
{
    public record EnumerableViewModel<T>(IEnumerable<T> Data)
    {
    }
}

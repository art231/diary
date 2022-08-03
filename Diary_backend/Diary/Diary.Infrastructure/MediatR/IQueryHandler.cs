using Diary.Domain.Models;
using Diary.Infrastructure.Queries;
using MediatR;

namespace Diary.Infrastructure.MediatR
{
    public abstract record Query<T> : Message, IRequest<T>
    {
    }

    public abstract record QueryWithPagination<T> : Query<T>
    {
        public Pagination Pagination { get; init; } = new();

        public int Skip()
        {
            return Pagination.Skip();
        }
        public int Take()
        {
            return Pagination.Take();
        }
    }

    public interface IQueryHandler<in TQuery, TQueryResponse> : IRequestHandler<TQuery, TQueryResponse>
        where TQuery : Query<TQueryResponse>
    {
    }
}

using AutoMapper;
using Diary.Domain.Models;
using Diary.Domain.Specifications.Base;
using Diary.Infrastructure.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Diary.Infrastructure.Queries
{
    public interface IQueryBase<TQueryModel> where TQueryModel : IAggregateRoot
    {
        IMapper Mapper { get; }
        DiaryDbContext Context { get; }
        Task<int> CountAsync(Expression<Func<TQueryModel, bool>> predicate);
        Task<int> CountAsync(Specification<TQueryModel> predicate);
        Task<bool> AnyAsync(Expression<Func<TQueryModel, bool>> predicate);
        Task<bool> AnyAsync(Specification<TQueryModel> predicate);
        Task<List<TQuery>> GetAllAsync<TQuery>();
        Task<TContainer> GetAllAsync<TQuery, TContainer>();
        Task<TQuery?> GetAsync<TQuery>(Expression<Func<TQueryModel, bool>> predicate);
        Task<TQuery?> GetAsync<TQuery>(Specification<TQueryModel> predicate);
        Task<TContainer> GetAsync<TQuery, TContainer>(Expression<Func<TQueryModel, bool>> predicate);
        Task<TContainer> GetAsync<TQuery, TContainer>(Specification<TQueryModel> predicate);
        Task<List<TQuery>> GetManyAsync<TQuery>(Expression<Func<TQueryModel, bool>> predicate);
        Task<List<TQuery>> GetManyAsync<TQuery>(Specification<TQueryModel> predicate);
        Task<TContainer> GetManyAsync<TQuery, TContainer>(Expression<Func<TQueryModel, bool>> predicate);
        Task<TContainer> GetManyAsync<TQuery, TContainer>(Specification<TQueryModel> predicate);

        Task<List<TQuery>> GetPaginatedAsync<TQuery>(
            Expression<Func<TQueryModel, bool>> predicate,
            Pagination pagination,
            IEnumerable<SortDescriptor> sortDescriptors,
            params Expression<Func<TQueryModel, object>>[] includeProperties);

        Task<List<TQuery>> GetPaginatedAsync<TQuery>(Specification<TQueryModel> predicate,
            Pagination pagination,
            IEnumerable<SortDescriptor> sortDescriptors,
            params Expression<Func<TQueryModel, object>>[] includeProperties);

        Task<PaginatedViewModel<TQuery>> GetPaginatedWithMetadataAsync<TQuery>(
            Expression<Func<TQueryModel, bool>> predicate,
            Pagination pagination,
            IEnumerable<SortDescriptor> sortDescriptors,
            params Expression<Func<TQueryModel, object>>[] includeProperties);

        Task<PaginatedViewModel<TQuery>> GetPaginatedWithMetadataAsync<TQuery>(Specification<TQueryModel> predicate,
            Pagination pagination,
            IEnumerable<SortDescriptor> sortDescriptors,
            params Expression<Func<TQueryModel, object>>[] includeProperties);

        Task<TContainer> GetManyOrderedAsync<TQuery, TContainer, TKey>(Expression<Func<TQueryModel, bool>> predicate,
            Expression<Func<TQueryModel, TKey>> keySelector);

        Task<TContainer> GetManyOrderedAsync<TQuery, TContainer, TKey>(Specification<TQueryModel> predicate,
            Expression<Func<TQueryModel, TKey>> keySelector);

        Task<List<TQuery>> GetManyOrderedAsync<TQuery, TKey>(Expression<Func<TQueryModel, bool>> predicate,
            Expression<Func<TQueryModel, TKey>> keySelector);

        Task<List<TQuery>> GetManyOrderedAsync<TQuery, TKey>(Specification<TQueryModel> predicate,
            Expression<Func<TQueryModel, TKey>> keySelector);

        Task<List<TQuery>> GetAllOrderedAsync<TQuery, TKey>(Expression<Func<TQueryModel, TKey>> keySelector);
        Task<List<TQuery>> GetAllOrderedAsync<TQuery, TKey>(Specification<TQueryModel> keySelector);
        Task<TContainer> GetAllOrderedAsync<TQuery, TContainer, TKey>(Expression<Func<TQueryModel, TKey>> keySelector);
        Task<TContainer> GetAllOrderedAsync<TQuery, TContainer, TKey>(Specification<TQueryModel> keySelector);
    }
}

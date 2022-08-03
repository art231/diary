using AutoMapper;
using AutoMapper.QueryableExtensions;
using Diary.Domain.Models;
using Diary.Domain.Specifications.Base;
using Diary.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Diary.Infrastructure.Queries
{
    public class QueryBase<TQueryModel> : IQueryBase<TQueryModel> where TQueryModel : class, IAggregateRoot
    {
        public QueryBase(IMapper mapper, DiaryDbContext context)
        {
            Mapper = mapper;
            Context = context;
            Query = context.Set<TQueryModel>().AsNoTracking();
        }

        public IMapper Mapper { get; }
        public DiaryDbContext Context { get; }
        private IQueryable<TQueryModel> Query { get; }

        public virtual async Task<TContainer> GetAllAsync<TQuery, TContainer>()
        {
            return Mapper.Map<TContainer>(await GetAllAsync<TQuery>());
        }

        public Task<List<TQuery>> GetAllOrderedAsync<TQuery, TKey>(Specification<TQueryModel> keySelector)
        {
            return Query.OrderBy(keySelector.ToExpression()).ProjectTo<TQuery>(Mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public virtual async Task<TContainer>
            GetAllOrderedAsync<TQuery, TContainer, TKey>(Expression<Func<TQueryModel, TKey>> keySelector)
        {
            return Mapper.Map<TContainer>(await GetAllOrderedAsync<TQuery, TKey>(keySelector));
        }

        public async Task<TContainer> GetAllOrderedAsync<TQuery, TContainer, TKey>(
            Specification<TQueryModel> keySelector)
        {
            return Mapper.Map<TContainer>(await GetAllOrderedAsync<TQuery, TKey>(keySelector));
        }

        public virtual Task<bool> AnyAsync(Specification<TQueryModel> predicate)
        {
            return AnyAsync(predicate.ToExpression());
        }

        public virtual Task<List<TQuery>> GetAllAsync<TQuery>()
        {
            return Query.ProjectTo<TQuery>(Mapper.ConfigurationProvider).ToListAsync();
        }

        public virtual Task<List<TQuery>> GetManyOrderedAsync<TQuery, TKey>(Specification<TQueryModel> predicate,
            Expression<Func<TQueryModel, TKey>> keySelector)
        {
            return GetManyOrderedAsync<TQuery, TKey>(predicate.ToExpression(), keySelector);
        }

        public virtual Task<List<TQuery>>
            GetAllOrderedAsync<TQuery, TKey>(Expression<Func<TQueryModel, TKey>> keySelector)
        {
            return Query
                .OrderBy(keySelector)
                .ProjectTo<TQuery>(Mapper.ConfigurationProvider).ToListAsync();
        }

        public virtual Task<int> CountAsync(Expression<Func<TQueryModel, bool>> predicate)
        {
            return Query.CountAsync(predicate);
        }

        public virtual Task<int> CountAsync(Specification<TQueryModel> predicate)
        {
            return CountAsync(predicate.ToExpression());
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TQueryModel, bool>> predicate)
        {
            return Query.AnyAsync(predicate);
        }

        public virtual Task<TQuery?> GetAsync<TQuery>(Specification<TQueryModel> predicate)
        {
            return GetAsync<TQuery>(predicate.ToExpression());
        }

        public virtual async Task<TContainer> GetAsync<TQuery, TContainer>(
            Expression<Func<TQueryModel, bool>> predicate)
        {
            return Mapper.Map<TContainer>(await GetAsync<TQuery>(predicate));
        }

        public virtual Task<TContainer> GetAsync<TQuery, TContainer>(Specification<TQueryModel> predicate)
        {
            return GetAsync<TContainer>(predicate.ToExpression())!;
        }

        public virtual Task<TQuery?> GetAsync<TQuery>(Expression<Func<TQueryModel, bool>> predicate)
        {
            return GetWithProjectTo<TQuery?>(predicate).FirstOrDefaultAsync();
        }

        public virtual Task<List<TQuery>> GetManyAsync<TQuery>(Expression<Func<TQueryModel, bool>> predicate)
        {
            return GetWithProjectTo<TQuery>(predicate).ToListAsync();
        }

        public virtual Task<List<TQuery>> GetManyAsync<TQuery>(Specification<TQueryModel> predicate)
        {
            return GetManyAsync<TQuery>(predicate.ToExpression());
        }

        public virtual Task<TContainer> GetManyOrderedAsync<TQuery, TContainer, TKey>(
            Specification<TQueryModel> predicate,
            Expression<Func<TQueryModel, TKey>> keySelector)
        {
            return GetManyOrderedAsync<TQuery, TContainer, TKey>(predicate.ToExpression(), keySelector);
        }

        public virtual Task<List<TQuery>> GetManyOrderedAsync<TQuery, TKey>(
            Expression<Func<TQueryModel, bool>> predicate,
            Expression<Func<TQueryModel, TKey>> keySelector)
        {
            return GetWithProjectToOrdered<TQuery, TKey>(predicate, keySelector).ToListAsync();
        }

        public async Task<TContainer> GetManyAsync<TQuery, TContainer>(Expression<Func<TQueryModel, bool>> predicate)
        {
            return Mapper.Map<TContainer>(await GetManyAsync<TQuery>(predicate));
        }

        public virtual Task<TContainer> GetManyAsync<TQuery, TContainer>(Specification<TQueryModel> predicate)
        {
            return GetManyAsync<TQuery, TContainer>(predicate.ToExpression());
        }

        public virtual Task<PaginatedViewModel<TQuery>> GetPaginatedWithMetadataAsync<TQuery>(
            Specification<TQueryModel> predicate, Pagination pagination,
            IEnumerable<SortDescriptor> sortDescriptors,
            params Expression<Func<TQueryModel, object>>[] includeProperties)
        {
            return GetPaginatedWithMetadataAsync<TQuery>(predicate.ToExpression(), pagination, sortDescriptors,
                includeProperties);
        }

        public async Task<TContainer> GetManyOrderedAsync<TQuery, TContainer, TKey>(
            Expression<Func<TQueryModel, bool>> predicate,
            Expression<Func<TQueryModel, TKey>> keySelector)
        {
            return Mapper.Map<TContainer>(await GetManyOrderedAsync<TQuery, TKey>(predicate, keySelector));
        }

        public virtual Task<List<TQuery>> GetPaginatedAsync<TQuery>(
            Expression<Func<TQueryModel, bool>> predicate,
            Pagination pagination,
            IEnumerable<SortDescriptor> sortDescriptors,
            params Expression<Func<TQueryModel, object>>[] includeProperties)
        {
            return GetPaginatedAsQueryable(predicate, pagination, sortDescriptors, includeProperties)
                .ProjectTo<TQuery>(Mapper.ConfigurationProvider).ToListAsync();
        }

        public virtual Task<List<TQuery>> GetPaginatedAsync<TQuery>(Specification<TQueryModel> predicate,
            Pagination pagination,
            IEnumerable<SortDescriptor> sortDescriptors,
            params Expression<Func<TQueryModel, object>>[] includeProperties)
        {
            return GetPaginatedAsQueryable(predicate.ToExpression(), pagination, sortDescriptors,
                    includeProperties)
                .ProjectTo<TQuery>(Mapper.ConfigurationProvider).ToListAsync();
        }

        public virtual async Task<PaginatedViewModel<TQuery>> GetPaginatedWithMetadataAsync<TQuery>(
            Expression<Func<TQueryModel, bool>> predicate,
            Pagination pagination,
            IEnumerable<SortDescriptor> sortDescriptors,
            params Expression<Func<TQueryModel, object>>[] includeProperties)
        {
            return new(pagination,
                await GetByPredicateOrDefault(predicate).CountAsync(),
                await GetPaginatedAsQueryable(predicate, pagination, sortDescriptors, includeProperties)
                    .ProjectTo<TQuery>(Mapper.ConfigurationProvider).ToListAsync());
        }

        private IQueryable<TQuery> GetWithProjectTo<TQuery>(Expression<Func<TQueryModel, bool>> predicate)
        {
            return Query.Where(predicate).ProjectTo<TQuery>(Mapper.ConfigurationProvider);
        }

        private IQueryable<TQuery> GetWithProjectToOrdered<TQuery, TKey>(Expression<Func<TQueryModel, bool>> predicate,
            Expression<Func<TQueryModel, TKey>> keySelector)
        {
            return Query.Where(predicate).OrderBy(keySelector).ProjectTo<TQuery>(Mapper.ConfigurationProvider);
        }

        private IQueryable<TQueryModel> GetByPredicateOrDefault(Expression<Func<TQueryModel, bool>> predicate)
        {
            return Query.Where(predicate);
        }

        private IQueryable<TQueryModel> GetPaginatedAsQueryable(Expression<Func<TQueryModel, bool>> predicate,
            Pagination paginatedRequest, IEnumerable<SortDescriptor> sortDescriptors,
            params Expression<Func<TQueryModel, object>>[] includeProperties)
        {
            var query = GetByPredicateOrDefault(predicate);
            query = PerformPaging(query, paginatedRequest, sortDescriptors);
            if (includeProperties.Length > 0)
                query = includeProperties.Aggregate(query,
                    (current, includeProperty) => current.Include(includeProperty));
            return query;
        }

        private static IQueryable<TQueryModel> PerformPaging(IQueryable<TQueryModel> query,
            Pagination paginatedRequest,
            IEnumerable<SortDescriptor> sortDescriptors)
        {
            var index = 0;
            foreach (var (field, sortDirection) in sortDescriptors)
            {
                if (string.IsNullOrWhiteSpace(field)) continue;

                switch (sortDirection)
                {
                    case SortDirection.Ascending:
                        query = CallMethod(query, index == 0 ? "OrderBy" : "ThenBy", field);
                        index++;
                        break;
                    case SortDirection.Descending:
                        query = CallMethod(query, index == 0 ? "OrderByDescending" : "ThenByDescending", field);
                        index++;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(sortDirection.ToString());
                }
            }

            return query.Skip(paginatedRequest.Skip()).Take(paginatedRequest.Take());
        }

        private static IOrderedQueryable<TQueryModel> CallMethod(IQueryable query, string methodName,
            string memberName)
        {
            ParameterExpression[] typeParams = { Expression.Parameter(typeof(TQueryModel), string.Empty) };
            var pi = typeof(TQueryModel).GetProperty(memberName);

            return (IOrderedQueryable<TQueryModel>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { typeof(TQueryModel), pi!.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
            );
        }
    }
}

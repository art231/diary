using Diary.Domain.Models;
using Diary.Domain.Specifications.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Diary.Domain.Contracts
{
    public interface IRepository<TEntity> where TEntity : IAggregateRoot
    {
        ValueTask AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateMany(IEnumerable<TEntity?> entities);
        void Delete(TEntity entity);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        void Delete(Specification<TEntity> predicate);
        void DeleteRange(IEnumerable<TEntity> entities);

        ValueTask<TEntity> GetByIdAsync(params object[] keyValues);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(Specification<TEntity> predicate);
        Task<List<TEntity>> GetAllAsync();

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> AnyAsync(Specification<TEntity> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);
        Task<int> CountAsync(Specification<TEntity>? predicate = null);

        Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetManyAsync(Specification<TEntity> predicate);

        Task<Dictionary<TKey, TValue>> GetManyAsync<TKey, TValue>(Expression<Func<TEntity, bool>> predicate,
            Func<TEntity, TKey> keySelector, Func<TEntity, TValue> valueSelector) where TKey : notnull;

        Task<Dictionary<TKey, TValue>> GetManyAsync<TKey, TValue>(Specification<TEntity> predicate,
            Func<TEntity, TKey> keySelector, Func<TEntity, TValue> valueSelector) where TKey : notnull;

        Task<HashSet<TKey>> GetManyAsync<TKey>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TKey>> keySelector) where TKey : notnull;

        Task<HashSet<TKey>> GetManyAsync<TKey>(Specification<TEntity> predicate,
            Expression<Func<TEntity, TKey>> keySelector) where TKey : notnull;
    }
}

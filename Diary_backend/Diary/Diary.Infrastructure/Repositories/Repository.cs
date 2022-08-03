using Diary.Domain.Contracts;
using Diary.Domain.Models;
using Diary.Domain.Specifications.Base;
using Diary.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Diary.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        protected readonly DiaryDbContext DbContext;

        protected readonly DbSet<TEntity> DbSet;

        public Repository(DiaryDbContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<TEntity>();
        }

        public virtual async ValueTask AddAsync(TEntity entity)
        {
            _ = await DbSet.AddAsync(entity);
        }

        public virtual Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return DbSet.AddRangeAsync(entities);
        }
        public void Update(TEntity entity)
        {
            var entry = DbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Modified;
            }
        }
        public void UpdateMany(IEnumerable<TEntity?> entities)
        {
            foreach (var entity in entities.Where(x => x != null))
            {
                Update(entity!);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void Delete(Specification<TEntity> predicate)
        {
            Delete(predicate.ToExpression());
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            DbSet.RemoveRange(DbSet.Where(predicate).AsEnumerable());
        }

        public virtual ValueTask<TEntity> GetByIdAsync(params object[] keyValues)
        {
            return DbSet.FindAsync(keyValues);
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefaultAsync(predicate);
        }

        public Task<TEntity> GetAsync(Specification<TEntity> predicate)
        {
            return GetAsync(predicate.ToExpression());
        }

        public virtual Task<List<TEntity>> GetAllAsync()
        {
            return DbSet.ToListAsync();
        }

        public Task<bool> AnyAsync(Specification<TEntity> predicate)
        {
            return AnyAsync(predicate.ToExpression());
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            return predicate == null ? DbSet.CountAsync() : DbSet.CountAsync(predicate);
        }

        public Task<int> CountAsync(Specification<TEntity>? predicate = null)
        {
            return CountAsync(predicate?.ToExpression());
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.AnyAsync(predicate);
        }

        public virtual Task<List<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).ToListAsync();
        }

        public Task<List<TEntity>> GetManyAsync(Specification<TEntity> predicate)
        {
            return GetManyAsync(predicate.ToExpression());
        }

        public virtual Task<Dictionary<TKey, TValue>> GetManyAsync<TKey, TValue>(
            Expression<Func<TEntity, bool>> predicate,
            Func<TEntity, TKey> keySelector, Func<TEntity, TValue> valueSelector) where TKey : notnull
        {
            return DbSet.Where(predicate).ToDictionaryAsync(keySelector, valueSelector);
        }

        public virtual Task<Dictionary<TKey, TValue>> GetManyAsync<TKey, TValue>(Specification<TEntity> predicate,
            Func<TEntity, TKey> keySelector, Func<TEntity, TValue> valueSelector) where TKey : notnull
        {
            return DbSet.Where(predicate.ToExpression()).ToDictionaryAsync(keySelector, valueSelector);
        }

        public async Task<HashSet<TKey>> GetManyAsync<TKey>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TKey>> keySelector) where TKey : notnull
        {
            return (await DbSet.Where(predicate).Select(keySelector).ToListAsync()).ToHashSet();
        }

        public async Task<HashSet<TKey>> GetManyAsync<TKey>(Specification<TEntity> predicate,
            Expression<Func<TEntity, TKey>> keySelector) where TKey : notnull
        {
            return (await DbSet.Where(predicate.ToExpression()).Select(keySelector).ToListAsync()).ToHashSet();
        }
    }
}

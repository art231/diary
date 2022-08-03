using Diary.Domain.Aggregates;
using Diary.Domain.Aggregates.Notes;
using Diary.Domain.Aggregates.User;
using Diary.Domain.Models;
using Diary.Domain.Models.Interfaces;
using Diary.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Diary.Infrastructure.ApplicationDbContext
{
    public sealed class DiaryDbContext : IdentityDbContext<User,
            Role,
            Guid,
            IdentityUserClaim<Guid>,
            UserRole,
            IdentityUserLogin<Guid>,
            IdentityRoleClaim<Guid>,
            IdentityUserToken<Guid>>

    {
        private IDbContextTransaction? _currentTransaction;
        public DiaryDbContext(DbContextOptions<DiaryDbContext> options) : base(options) { }
        public bool HasActiveTransaction => _currentTransaction != null;

        public new DbSet<User> Users { get; private set; }
        public DbSet<Notes> Notes { get; private set; }

        public async Task<IDbContextTransaction?> BeginTransactionAsync(CancellationToken token = default)
        {
            if (_currentTransaction != null)
            {
                return null;
            }

            _currentTransaction = await Database.BeginTransactionAsync(token);
            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken token = default)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            if (transaction != _currentTransaction)
            {
                throw new InvalidOperationException($"Transaction [Id: {transaction.TransactionId}] is not current");
            }

            try
            {
                await SaveChangesAsync(token);
                await transaction.CommitAsync(token);
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                TranDispose();
            }
        }

        private void TranDispose()
        {
            if (_currentTransaction == null)
            {
                return;
            }

            _currentTransaction.Dispose();
            _currentTransaction = null;
        }

        private void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                TranDispose();
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = this.ChangeTracker
                .Entries()
                .Where(e => e.Entity is IDatedEntity &&
                            (e.State == EntityState.Added ||
                             e.State == EntityState.Modified));

            var now = DateTimeOffset.Now;
            foreach (var entityEntry in entries)
            {
                switch (entityEntry.State)
                {
                    case EntityState.Added:
                        ((IDatedEntity)entityEntry.Entity).CreatedAt = now;
                        ((IDatedEntity)entityEntry.Entity).UpdatedAt = now;
                        break;
                    case EntityState.Modified:
                        ((IDatedEntity)entityEntry.Entity).UpdatedAt = now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("diary");
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DiaryDbContext).Assembly);
            modelBuilder.Ignore<DomainEvent>();
            modelBuilder.ApplyGlobalFilters<IDeletable>(x => !x.IsDeleted);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TypeExtensions = AutoMapper.Internal.TypeExtensions;

namespace Diary.Infrastructure.DbContext
{

    public static class DbContextExtensions
    {
        public static void ApplyConfigurationsFromAssembly<T>(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var type = typeof(T);
            modelBuilder.ApplyConfigurationsFromAssembly(assembly,
                x => x.GetInterfaces().Any(n =>
                    n.IsGenericType &&
                    TypeExtensions.GetTypeDefinitionIfGeneric(n) == typeof(IEntityTypeConfiguration<>) &&
                    n.GetGenericArguments().Any(t => type.IsAssignableFrom(t))));
        }

        public static void ApplyGlobalFilters<TInterface>(this ModelBuilder modelBuilder,
            Expression<Func<TInterface, bool>> expression)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                if (entityType.ClrType.GetInterface(typeof(TInterface).Name) != null)
                {
                    var newParam = Expression.Parameter(entityType.ClrType);
                    var body = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam,
                        expression.Body);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(body, newParam));
                }
        }
    }
}

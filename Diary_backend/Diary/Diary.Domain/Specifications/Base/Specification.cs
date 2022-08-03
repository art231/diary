using System;
using System.Linq.Expressions;

namespace Diary.Domain.Specifications.Base
{
    public abstract class Specification<T>
    {
        public bool IsSatisfiedBy(T entity)
        {
            var predicate = ToExpression().Compile();
            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public Specification<T> And(Specification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }

        public Specification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }

    public class GenericSpecification<T>
    {
        public GenericSpecification(Expression<Func<T, bool>> expression)
        {
            Expression = expression;
        }

        private Expression<Func<T, bool>> Expression { get; }

        public bool IsSatisfiedBy(T entity)
        {
            return Expression.Compile().Invoke(entity);
        }
    }
}

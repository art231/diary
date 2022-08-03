using System;
using System.Linq.Expressions;

namespace Diary.Domain.Specifications.Base
{
    public sealed class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = _left.ToExpression();
            var rightExpression = _right.ToExpression();
            var invokedExpression = Expression.Invoke(rightExpression, leftExpression.Parameters);
            return (Expression<Func<T, bool>>)Expression.Lambda(
                Expression.OrElse(leftExpression.Body, invokedExpression), leftExpression.Parameters);
        }
    }
}

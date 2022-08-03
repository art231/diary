using Diary.Domain.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Diary.Infrastructure.MediatR.Base
{
    public abstract class HandlerBase
    {
        private readonly IUnitOfWork _uow;

        protected HandlerBase(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected Task CommitAsync(CancellationToken token)
        {
            return _uow.CommitAsync(token);
        }
    }
}

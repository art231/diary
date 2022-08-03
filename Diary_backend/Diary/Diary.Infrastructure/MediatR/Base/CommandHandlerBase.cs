using Diary.Domain.Contracts;
using Diary.Domain.Models;
using MediatR;

namespace Diary.Infrastructure.MediatR.Base
{
    public abstract record Command : CommandBase, IRequest<bool>
    {
    }

    public abstract record Command<T> : CommandBase, IRequest<T>
    {
    }

    public abstract record CommandBase : Message
    {
    }

    public abstract class CommandHandlerBase : HandlerBase
    {
        protected CommandHandlerBase(IUnitOfWork uow) : base(uow)
        {
        }
    }
}

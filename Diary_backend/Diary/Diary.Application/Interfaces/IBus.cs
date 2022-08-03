using Diary.Domain.Models;
using Diary.Infrastructure.MediatR;
using Diary.Infrastructure.MediatR.Base;
using System.Threading;
using System.Threading.Tasks;

namespace Diary.Application.Interfaces
{
    public interface IBus
    {
        Task RaiseEvent<TEvent>(TEvent message, CancellationToken cancellationToken = default)
            where TEvent : DomainEvent;

        Task Send(Command message, CancellationToken cancellationToken = default);
        Task<TCommand> Send<TCommand>(Command<TCommand> message, CancellationToken cancellationToken = default);
        Task<TQuery> Send<TQuery>(Query<TQuery> message, CancellationToken cancellationToken = default);
    }
}



using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Diary.Application.Interfaces;
using Diary.Domain.Models;
using Diary.Infrastructure.MediatR;
using Diary.Infrastructure.MediatR.Base;
using MediatR;

namespace Diary.Application
{
    public sealed class Bus : IBus
    {
        private readonly IMediator _mediator;

        public Bus(IMediator mediator)
        {
            _mediator = mediator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task RaiseEvent<TEvent>(TEvent message, CancellationToken cancellationToken = default)
            where TEvent : DomainEvent
        {
            return _mediator.Publish(new DomainEventWrapper<TEvent>().Wrap(message), cancellationToken);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TCommand> Send<TCommand>(Command<TCommand> message,
            CancellationToken cancellationToken = default)
        {
            return _mediator.Send(message, cancellationToken);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task Send(Command message, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(message, cancellationToken);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Task<TQuery> Send<TQuery>(Query<TQuery> message, CancellationToken cancellationToken = default)
        {
            return _mediator.Send(message, cancellationToken);
        }
    }
}
using Diary.Domain.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Diary.Infrastructure.MediatR
{
    public interface IEventHandler<in TEvent> where TEvent : DomainEvent
    {
        Task Handle(TEvent message, CancellationToken cancellationToken = default);
    }

    public class DomainEventHandlerWrapper<TWrapper, TEvent> : INotificationHandler<TWrapper>
        where TWrapper : DomainEventWrapper<TEvent>, INotification
        where TEvent : DomainEvent

    {
        private readonly IEventHandler<TEvent> _handler;

        public DomainEventHandlerWrapper(IEventHandler<TEvent> handler)
        {
            _handler = handler;
        }

        public Task Handle(TWrapper wrapper, CancellationToken cancellationToken)
        {
            return _handler.Handle(wrapper.Event, cancellationToken);
        }
    }
}

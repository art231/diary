using MediatR;
using Diary.Domain.Models;
using System;

namespace Diary.Infrastructure.MediatR
{
    public interface IDomainEventWrapper : INotification
    {
        Type GetEventType();
        IDomainEventWrapper Wrap(DomainEvent domainEvent);
    }

    public record DomainEventWrapper<T> : IDomainEventWrapper where T : DomainEvent
    {
        public T Event { get; private set; }

        public IDomainEventWrapper Wrap(DomainEvent domainEvent)
        {
            Event = (T)domainEvent;
            return this;
        }

        public Type GetEventType()
        {
            return typeof(T);
        }
    }
}

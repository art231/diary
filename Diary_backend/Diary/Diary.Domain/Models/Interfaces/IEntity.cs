using System.Collections.Generic;

namespace Diary.Domain.Models.Interfaces
{
    public interface IEntity
    {
        IEnumerable<DomainEvent> GetUncommittedChanges { get; }
        void AddDomainEvent(DomainEvent domainEvent);
        void MarkChangesAsCommitted();
        void RemoveDomainEvent(DomainEvent domainEvent);
    }
}

using Diary.Domain.Models.Interfaces;
using System.Collections.Generic;

namespace Diary.Domain.Models
{
    public abstract class Entity<TKey> : IEntity where TKey : struct
    {
        private readonly List<DomainEvent> _domainEvents = new();

        public TKey Id { get; private set; }
        public IEnumerable<DomainEvent> GetUncommittedChanges => _domainEvents.AsReadOnly();

        public void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void MarkChangesAsCommitted()
        {
            _domainEvents.Clear();
        }

        public override bool Equals(object? obj)
        {
            var compareTo = obj as Entity<TKey>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity<TKey>? a, Entity<TKey>? b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TKey> a, Entity<TKey>? b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() ^ 93) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
    }
}

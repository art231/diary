using Diary.Domain.Models.Interfaces;
using Diary.Infrastructure.ApplicationDbContext;
using Diary.Infrastructure.DbContext;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Diary.Infrastructure.MediatR
{
    internal static class MediatorExtension
    {
        public static async Task DispatchDomainEventsAsync(this IPublisher publisher, DiaryDbContext context,
            IServiceProvider service)
        {
            var domainEntities = context.ChangeTracker
                .Entries<IEntity>()
                .Where(x => x.Entity.GetUncommittedChanges.Any()).ToList();

            if (!domainEntities.Any()) return;

            var domainEvents = domainEntities.SelectMany(x => x.Entity.GetUncommittedChanges).ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.MarkChangesAsCommitted());

            var eventWrappers = service.GetServices<IDomainEventWrapper>().ToDictionary(x => x.GetEventType().Name);
            foreach (var wrapper in domainEvents.Select(x => eventWrappers[x.GetMessageType()].Wrap(x)))
                await publisher.Publish(wrapper);
        }
    }
}

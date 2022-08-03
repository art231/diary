using Diary.Domain.Contracts;
using Diary.Infrastructure.MediatR;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Diary.Infrastructure.ApplicationDbContext
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DiaryDbContext _context;
        private readonly IPublisher _mediator;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(DiaryDbContext context, IPublisher mediator, IServiceProvider serviceProvider)
        {
            _context = context;
            _mediator = mediator;
            _serviceProvider = serviceProvider;
        }

        public async Task CommitAsync(CancellationToken token)
        {
            await _mediator.DispatchDomainEventsAsync(_context, _serviceProvider);
            await _context.SaveChangesAsync(token);
        }
    }
}

using Diary.Application.Queries.User;
using Diary.Domain.Exceptions;
using Diary.Infrastructure.MediatR;
using Diary.Infrastructure.MediatR.Base;
using Diary.Infrastructure.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Diary.Application.Queries.Notes
{
    public sealed class NotesQueryHandler : QueryHandlerBase,
        IQueryHandler<GetNotesQuery, NotesViewModel>,
        IQueryHandler<GetListNotesQuery, PaginatedViewModel<NotesViewModel>>
    {
        private readonly IQueryBase<Diary.Domain.Aggregates.Notes.Notes> notesQuery;
        public NotesQueryHandler(IQueryBase<Diary.Domain.Aggregates.Notes.Notes> notesQuery)
        {
            this.notesQuery = notesQuery;
        }
        public async Task<NotesViewModel> Handle(GetNotesQuery request, CancellationToken cancellationToken)
        {
            var result = await this.notesQuery.GetAsync<NotesViewModel>(x => x.Id == request.Id);
            if(result==null)
            {
                throw new DomainException($"GetNotesQuery is null");
            }
            return result;
        }

        public async Task<PaginatedViewModel<NotesViewModel>> Handle(GetListNotesQuery request, CancellationToken cancellationToken)
        {
            var result = await this.notesQuery.GetPaginatedWithMetadataAsync<NotesViewModel>(x => request.UserId == x.UserId, request.Pagination,
                new[] { new SortDescriptor(nameof(NotesViewModel.Id), SortDirection.Descending) });
            if (result == null)
            {
                throw new DomainException($"GetUserQuery is null");
            }
            return result;
        }
    }

}

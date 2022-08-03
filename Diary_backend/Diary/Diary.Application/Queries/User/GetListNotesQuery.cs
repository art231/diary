using Diary.Application.Queries.Notes;
using Diary.Infrastructure.MediatR;
using Diary.Infrastructure.Queries;
using System;

namespace Diary.Application.Queries.User
{
    public sealed record GetListNotesQuery : QueryWithPagination<PaginatedViewModel<NotesViewModel>>
    {
        public Guid UserId { get; init; }
    }
}

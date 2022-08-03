using Diary.Infrastructure.MediatR;
using FluentValidation;
using System;

namespace Diary.Application.Queries.Notes
{
    public sealed record GetNotesQuery : Query<NotesViewModel>
    {
        public int Id { get; init; }
    }
    public sealed class GetNotesValidator : AbstractValidator<GetNotesQuery>
    {
        public GetNotesValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
    public sealed record NotesViewModel()
    {
        public int Id { get; init; }
        public Guid UserId { get; init; }
        /// <summary>
        /// дата назначения
        /// </summary>
        public DateTime InitialDate { get; init; }
        /// <summary>
        /// Тема
        /// </summary>
        public string Title { get; init; } = string.Empty;
        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; init; }
        public bool IsDeleted { get; init; }
    }
}

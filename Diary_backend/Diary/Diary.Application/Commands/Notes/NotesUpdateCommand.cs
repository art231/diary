using Diary.Infrastructure.MediatR.Base;
using FluentValidation;
using System;

namespace Diary.Application.Commands.Notes
{
    public sealed record NotesUpdateCommand : Command
    {
        public int Id { get; init; }
        /// <summary>
        /// Id пользователя
        /// </summary>
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
    public sealed class NotesUpdateCommandValidator : AbstractValidator<NotesUpdateCommand>
    {
        public NotesUpdateCommandValidator()
        {
            RuleFor(x => x.InitialDate).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}

using Diary.Infrastructure.MediatR.Base;
using FluentValidation;
using System;

namespace Diary.Application.Commands.Notes
{
    public sealed record NotesCreateCommand(Guid UserId, DateTime InitialDate, string Title, string? Description) : Command
    {
    }
    public sealed class NotesCreateCommandValidator : AbstractValidator<NotesCreateCommand>
    {
        public NotesCreateCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.InitialDate).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}

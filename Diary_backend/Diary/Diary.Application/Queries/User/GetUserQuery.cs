using Diary.Infrastructure.MediatR;
using FluentValidation;
using System;

namespace Diary.Application.Queries.User
{
    public sealed record GetUserQuery : Query<UserViewModel>
    {
        public GetUserQuery(Guid id)
        {
            this.Id = id;
        }
        public Guid Id { get; init; }
    }
    public sealed class GetUserValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
    public sealed record UserViewModel()
    {
        public Guid Id { get; init; }
        public string Login { get; init; } = string.Empty;
        public string PhoneNumber { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
        public string UserSecondName { get; init; } = string.Empty;
        public bool AcceptedAgreement { get; init; }
        public bool IsDeleted { get; init; }
    }
}

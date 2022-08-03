using Diary.Infrastructure.MediatR;
using FluentValidation;

namespace Diary.Application.Queries.Auth
{
    public sealed record GetAuthQuery : Query<AuthViewModel>
    {
        public int Id { get; init; }
    }
    public sealed record AuthViewModel
    {
        public int Id { get; private set; }
        public string Login { get; private set; } = string.Empty;
        public string UserSecondName { get; private set; } = string.Empty;
        public bool AcceptedAgreement { get; private set; }
        public bool IsDeleted { get; private set; }
    }
    public sealed class GetExpirationValidator : AbstractValidator<GetAuthQuery>
    {
        public GetExpirationValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}

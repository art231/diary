using Diary.Infrastructure.MediatR;
using Diary.Infrastructure.MediatR.Base;
using Diary.Infrastructure.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Diary.Application.Queries.Auth
{
    
    public sealed class AuthQueryHandler : QueryHandlerBase,
        IQueryHandler<GetAuthQuery, AuthViewModel>

    {
        private readonly IQueryBase<Diary.Domain.Aggregates.User.User> authQuery;

        public AuthQueryHandler(
            IQueryBase<Diary.Domain.Aggregates.User.User> authQuery
            )
        {
            this.authQuery = authQuery;
        }

        public async Task<AuthViewModel> Handle(
            GetAuthQuery request, CancellationToken cancellationToken)
        {
            return new AuthViewModel();
        }
    }
}

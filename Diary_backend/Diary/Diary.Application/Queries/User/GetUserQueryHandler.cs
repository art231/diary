using Diary.Domain.Exceptions;
using Diary.Infrastructure.MediatR;
using Diary.Infrastructure.MediatR.Base;
using Diary.Infrastructure.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace Diary.Application.Queries.User
{
    public sealed class GetUserQueryHandler : QueryHandlerBase,
        IQueryHandler<GetUserQuery, UserViewModel>
    {
        private readonly IQueryBase<Diary.Domain.Aggregates.User.User> userQuery;
        public GetUserQueryHandler(IQueryBase<Diary.Domain.Aggregates.User.User> userQuery)
        {
            this.userQuery = userQuery;
        }
        public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var result = await this.userQuery.GetAsync<UserViewModel>(x => x.Id == request.Id);
            if (result == null)
            {
                throw new DomainException($"GetUserQuery is null");
            }
            return result;
        }
    }
}

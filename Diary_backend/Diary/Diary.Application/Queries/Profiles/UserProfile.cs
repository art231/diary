using AutoMapper;
using Diary.Application.Queries.User;

namespace Diary.Application.Queries.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Diary.Domain.Aggregates.User.User, UserViewModel>();
        }
    }
}

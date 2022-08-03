using AutoMapper;
using Diary.Application.Commands.Auth;
using Diary.Application.Commands.Notes;
using Diary.Domain.Aggregates.Notes;
using Diary.Domain.Aggregates.User;

namespace Diary.Application.Profiles
{
    public class DiaryProfile : Profile
    {
        public DiaryProfile()
        {
            CreateMap<User, TokenUserViewModel>();
            CreateMap<NotesUpdateCommandModifier, Notes>();
        }
    }
}

using AutoMapper;
using Diary.Application.Queries.Notes;

namespace Diary.Application.Queries.Profiles
{
    public class NotesProfile : Profile
    {
        public NotesProfile()
        {
            CreateMap<Diary.Domain.Aggregates.Notes.Notes, NotesViewModel>();
        }
    }
}

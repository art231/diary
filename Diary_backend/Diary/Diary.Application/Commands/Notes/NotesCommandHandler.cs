using AutoMapper;
using Diary.Domain.Contracts;
using Diary.Infrastructure.MediatR;
using Diary.Infrastructure.MediatR.Base;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Diary.Application.Commands.Notes
{
    public sealed class NotesCommandHandler : CommandHandlerBase,
        ICommandHandler<NotesCreateCommand>,
        ICommandHandler<NotesUpdateCommand>
    {
        private readonly IRepository<Diary.Domain.Aggregates.Notes.Notes> notesRepository;
        private readonly IMapper mapper;
        public NotesCommandHandler(IUnitOfWork uow,
            IRepository<Diary.Domain.Aggregates.Notes.Notes> notesRepository,
            IMapper mapper) : base(uow)
        {
            this.notesRepository = notesRepository;
            this.mapper = mapper;
        }
        /// <summary>
        /// Создание заметки
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(NotesCreateCommand request, CancellationToken cancellationToken)
        {
            var (userId, initialDate, title, description) = request;

            var notes = Diary.Domain.Aggregates.Notes.Notes.Create(userId, initialDate, title, description);
            await this.notesRepository.AddAsync(notes);
            await CommitAsync(cancellationToken);
            return true;
        }
        /// <summary>
        /// Изменение заметки
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(NotesUpdateCommand request, CancellationToken cancellationToken)
        {
            var notes = await this.notesRepository.GetAsync(x => x.Id == request.Id);
            var modifiedRequest = new NotesUpdateCommandModifier
            {
                Id = request.Id,
                InitialDate = request.InitialDate != notes.InitialDate
                ? request.InitialDate
                : null,
                UserId = request.UserId != notes.UserId
                ? request.UserId
                : null,
                Title = request.Title != notes.Title
                ? request.Title
                : notes.Title,
                Description = request.Description != notes.Description
                ? request.Description
                : null,
                IsDeleted = request.IsDeleted != notes.IsDeleted
                ? request.IsDeleted
                : null
            };
            this.mapper.Map(modifiedRequest, notes);
            this.notesRepository.Update(notes);
            await CommitAsync(cancellationToken);
            return true;
        }
    }
}

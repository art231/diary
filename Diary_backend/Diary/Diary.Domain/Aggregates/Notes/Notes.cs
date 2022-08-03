using Diary.Domain.Models;
using Diary.Domain.Models.Interfaces;
using System;

namespace Diary.Domain.Aggregates.Notes
{
    public sealed class Notes : Entity<int>, IAggregateRoot, IDeletable
    {
        private Notes(Guid userId, DateTime initialDate, string title, string? description)
        {
            UserId = userId;
            InitialDate = initialDate;
            Title = title;
            Description = description;
        }
        /// <summary>
        /// Фабрика создания заметки
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="initialDate"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="isDelete"></param>
        /// <returns></returns>
        public static Notes Create(Guid userId, DateTime initialDate, string title, string? description)
        {
            return new (userId, initialDate, title, description);
        }

        /// <summary>
        /// Фабрика обновления заметки
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="initialDate"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="isDelete"></param>
        public void Update(Guid userId, DateTime initialDate, string title, string? description, bool isDelete)
        {
            UserId = userId;
            InitialDate = initialDate;
            Title = title;
            Description = description;
            IsDeleted = isDelete;
        }
        /// <summary>
        /// Id пользователя
        /// </summary>
        public Guid UserId { get; private set; }
        /// <summary>
        /// дата назначения
        /// </summary>
        public DateTime InitialDate { get; private set; }
        /// <summary>
        /// Тема
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string? Description { get; private set; }
        public bool IsDeleted { get; private set; }
    }
}

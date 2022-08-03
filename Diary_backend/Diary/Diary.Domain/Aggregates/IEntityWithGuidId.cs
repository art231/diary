using System;

namespace Diary.Domain.Aggregates
{
    public interface IEntityWithGuidId
    {
        public Guid Id { get; set; }
    }
}

using System;

namespace Diary.Domain.Aggregates
{
    public interface IDatedEntity
    {
        DateTimeOffset CreatedAt { get; set; }

        DateTimeOffset UpdatedAt { get; set; }
    }
}

namespace Diary.Domain.Models.Interfaces
{
    /// <summary>
    ///     Признак "удаляемой" сущности
    /// </summary>
    public interface IDeletable
    {
        /// <summary>
        ///     Признак "удален"
        /// </summary>
        bool IsDeleted { get; }
    }
}

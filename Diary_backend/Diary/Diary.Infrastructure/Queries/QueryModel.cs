namespace Diary.Infrastructure.Queries
{
    public interface IQueryModel
    {
    }
    public abstract record QueryModel<TKey> : IQueryModel where TKey : struct
    {
        public TKey Id { get; init; }
    }
}

namespace Diary.Infrastructure.Queries
{
    public record SortDescriptor(string Field, SortDirection Direction)
    {
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}

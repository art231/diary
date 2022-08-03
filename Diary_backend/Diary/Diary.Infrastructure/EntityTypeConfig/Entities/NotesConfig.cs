using Diary.Domain.Aggregates.Notes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diary.Infrastructure.EntityTypeConfig.Entities
{
    public sealed class NotesConfig : IEntityTypeConfiguration<Notes>
    {
        public void Configure(EntityTypeBuilder<Notes> builder)
        {
            builder.ToTable(nameof(Notes));
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserId);
        }
    }
}

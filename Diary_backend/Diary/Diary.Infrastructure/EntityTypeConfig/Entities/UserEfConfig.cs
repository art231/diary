using Diary.Domain.Aggregates.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diary.Infrastructure.EntityTypeConfig.Entities
{
    public sealed class UserEfConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasValueGenerator<StringGuidValueGenerator>();
            builder.Property(x => x.UserSecondName).HasMaxLength(100);
        }
    }
}

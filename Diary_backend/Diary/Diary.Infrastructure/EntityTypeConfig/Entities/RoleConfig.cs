using Diary.Domain.Aggregates.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diary.Infrastructure.EntityTypeConfig.Entities
{
    internal sealed class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(nameof(Role));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasValueGenerator<StringGuidValueGenerator>();
        }
    }
}

using Diary.Domain.Aggregates.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Diary.Infrastructure.EntityTypeConfig.Entities
{
    internal sealed class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable(nameof(UserRole));

            builder.HasKey(ur => new { ur.UserId, ur.RoleId });

            //builder.HasOne(ur => ur.Role)
            //    .WithMany(r => r.UserRoles)
            //    .HasForeignKey(ur => ur.RoleId)
            //    .IsRequired();

            //builder.HasOne(ur => ur.User)
            //    .WithMany(r => r.UserRoles)
            //    .HasForeignKey(ur => ur.UserId)
            //    .IsRequired();
        }
    }
}

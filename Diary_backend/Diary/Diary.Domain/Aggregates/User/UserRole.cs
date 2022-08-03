using Microsoft.AspNetCore.Identity;
using System;

namespace Diary.Domain.Aggregates.User
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public new Guid UserId { get; private set; }

        public new Guid RoleId { get; private set; }
        public User User { get; private set; } = new();

        public Role Role { get; private set; } = new();
    }
}

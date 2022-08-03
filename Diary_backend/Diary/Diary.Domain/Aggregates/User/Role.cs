using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Diary.Domain.Aggregates.User
{
    public class Role : IdentityRole<Guid>, IEntityWithGuidId
    {
        public new string Name { get; private set; } = string.Empty;

        public List<UserRole> UserRoles { get; private set; } = new();
    }
}

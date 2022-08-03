using Diary.Domain.Models;
using Diary.Domain.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diary.Domain.Aggregates.User
{
    public class User : IdentityUser<Guid>, IDeletable, IAggregateRoot, IEntityWithGuidId
    {
        public string Login { get; private set; } = string.Empty;
        public string UserSecondName { get; private set; } = string.Empty;
        public bool AcceptedAgreement { get; private set; }
        public bool IsDeleted { get; private set; }
        public List<UserRole> UserRoles { get; private set; } = new();
    }
}

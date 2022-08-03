using Diary.Core.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Diary.Core.Models.EF;

public class User : IdentityUser, IEntityWithGuidId
{
    public string UserValueName { get; set; } = string.Empty;

    public string UserValueSecondName { get; set; } = string.Empty;

    public bool AcceptedUserAgreement { get; set; }
}

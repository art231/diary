using System;

namespace Diary.Api.Models.View
{
    public class TokenUserViewModel
    {
        public Guid Id { get; set; }

        public string Login { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
        public string UserSecondName { get; init; } = string.Empty;
        public bool AcceptedAgreement { get; init; }
        public bool IsDeleted { get; init; }
    }
}

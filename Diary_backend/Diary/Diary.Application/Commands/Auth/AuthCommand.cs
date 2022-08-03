using FluentValidation;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Diary.Application.Commands.Auth
{

    public sealed record AuthCommand : IRequest<TokenViewModel>//Command
    {
        [Required(ErrorMessage = "Поле PhoneNumber обязательно для заполнения")]
        [RegularExpression(@"^(?:\+7)\d{10}$", ErrorMessage = "Номер телефона введен неверно")]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
    public sealed class AuthValidator : AbstractValidator<AuthCommand>
    {
        public AuthValidator()
        {
            RuleFor(x => x.Password).NotEmpty()
                          .NotNull()
                          .MinimumLength(8)
                          .MaximumLength(16)
                          .Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$").WithMessage("Not correct Password"); ;
        }
    }
    public record TokenViewModel
    {
        public TokenUserViewModel User { get; init; } = new();

        public string Token { get; init; } = string.Empty;

        public string Message { get; init; } = string.Empty;
    }
}

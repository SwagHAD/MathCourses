using Application.Commands.Auth;
using FluentValidation;

namespace Application.Validators.Auth
{
    public sealed class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Логин не может быть пустым.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль обязателен.");
        }
    }
}

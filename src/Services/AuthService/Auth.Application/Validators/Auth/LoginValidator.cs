using Application.Commands.Auth;
using FluentValidation;

namespace Application.Validators.Auth
{
    public sealed class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Data.Login)
                .NotEmpty().WithMessage("Логин не может быть пустым.");
            RuleFor(x => x.Data.Password)
                .NotEmpty().WithMessage("Пароль обязателен.");
        }
    }
}

using Application.Commands.Update;
using FluentValidation;

namespace Application.Validators.Update
{
    public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("Id не может быть пустым");
            RuleFor(c => c.Login)
                .NotEmpty()
                .WithMessage("Логин не может быть пустым");
            RuleFor(c => c.PasswordHash)
                .NotEmpty()
                .WithMessage("Пароль не может быть пустым");
        }
    }
}

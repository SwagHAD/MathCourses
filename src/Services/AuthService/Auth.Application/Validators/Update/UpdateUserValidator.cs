using Application.Commands.Update;
using FluentValidation;

namespace Application.Validators.Update
{
    public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            RuleFor(c => c.Data.Id)
                .NotEmpty()
                .WithMessage("Id не может быть пустым");
            RuleFor(c => c.Data.Login)
                .NotEmpty()
                .WithMessage("Логин не может быть пустым");
            RuleFor(c => c.Data.Password)
                .MinimumLength(3)
                .When(c => c.Data.Password is not null)
                .WithMessage("Пароль слишком короткий");
        }
    }
}

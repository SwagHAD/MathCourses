using Application.Commands.Create;
using FluentValidation;

namespace Application.Validators.Create
{
    public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator() 
        {
            RuleFor(c => c.Login).NotEmpty().WithMessage("Логин не может быть пустым");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Пароль не может быть пустым");
        }
    }
}

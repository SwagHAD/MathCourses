using Application.Commands.Create;
using FluentValidation;

namespace Application.Validators.Create
{
    public sealed class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator() 
        {
            RuleFor(c => c.Data.Login).NotEmpty().WithMessage("Логин не может быть пустым");
            RuleFor(c => c.Data.Password).NotEmpty().WithMessage("Пароль не может быть пустым");
        }
    }
}

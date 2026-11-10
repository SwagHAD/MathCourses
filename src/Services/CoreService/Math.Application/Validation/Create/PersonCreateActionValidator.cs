using Application.Commands.CreateCommands;
using FluentValidation;

namespace Application.Validation.Create
{
    public sealed class PersonCreateActionValidator : AbstractValidator<CreatePersonCommand>
    {
        public PersonCreateActionValidator()
        {
            RuleFor(x => x.Data.Name)
                .NotEmpty().WithMessage("Имя обязательно")
                .MaximumLength(50).WithMessage("Имя слишком длинное");
            RuleFor(x => x.Data.Surname)
                .MaximumLength(50).WithMessage("Фамилия слишком длинная");
            RuleFor(x => x.Data.Patronymic)
                .MaximumLength(50).WithMessage("Отчество слишком длинное");
            RuleFor(x => x.Data.Email)
                .MaximumLength(100).EmailAddress().When(x => !string.IsNullOrEmpty(x.Data.Email))
                .WithMessage("Некорректный email");
            RuleFor(x => x.Data.Phone)
                .MaximumLength(50).WithMessage("Телефон слишком длинный");
            When(x => x.Data.Account is not null, () =>
            {
                RuleFor(x => x.Data.Account!.Login)
                    .NotEmpty().WithMessage("Логин обязателен");
                RuleFor(x => x.Data.Account!.Password)
                    .NotEmpty().WithMessage("Пароль обязателен");
                RuleFor(x => x.Data.Account!.RoleId)
                    .GreaterThan(0).WithMessage("Роль обязательна");
            });
        }
    }
}

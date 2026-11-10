using Application.Commands.UpdateCommands;
using FluentValidation;

namespace Application.Validation.Update
{
    public sealed class PersonUpdateActionValidator : AbstractValidator<UpdatePersonCommand>
    {
        public PersonUpdateActionValidator()
        {
            RuleFor(x => x.Data.Id)
                .GreaterThan(0).WithMessage("Идентификатор должен быть больше 0");
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
        }
    }
}

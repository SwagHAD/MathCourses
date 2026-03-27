using Application.Commands.CreateCommands;
using Application.Validation.Base;
using FluentValidation;

namespace Application.Validation.Create
{
    public sealed class GroupCreateActionValidator : BaseValidator<CreateGroupCommand>
    {
        public GroupCreateActionValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(50).WithMessage("Название слишком длинное");
            RuleFor(x => x.CourseID)
                .NotNull().WithMessage("Курс обязателен")
                .GreaterThan(0).WithMessage("Идентификатор курса невадиден");
            RuleFor(x => x.Students)
                .NotNull()
                .NotEmpty()
                .WithMessage("Список студентов не должен быть пустым.")
                .Must(list => !list.Any(id => id < 0))
                .WithMessage("Идентификаторы студентов невалидны");
            RuleFor(x => x.Teachers)
                .NotNull()
                .NotEmpty()
                .WithMessage("Список преподавателей не должен быть пустым.")
                .Must(list => !list.Any(id => id < 0))
                .WithMessage("Идентификаторы преподавателей невалидны");
        }
    }
}

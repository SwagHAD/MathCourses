using Application.Commands.CreateCommands;
using FluentValidation;

namespace Application.Validation.Create
{
    public sealed class GroupCreateActionValidator : AbstractValidator<CreateGroupCommand>
    {
        public GroupCreateActionValidator() 
        {
            RuleFor(x => x.Data.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(50).WithMessage("Название слишком длинное");
            RuleFor(x => x.Data.CourseId)
                .NotNull().WithMessage("Курс обязателен")
                .GreaterThan(0).WithMessage("Идентификатор курса невадиден");
            RuleFor(x => x.Data.StudentIds)
                .NotNull()
                .NotEmpty()
                .WithMessage("Список студентов не должен быть пустым.")
                .Must(list => !list.Any(id => id < 0))
                .WithMessage("Идентификаторы студентов невалидны");
            RuleFor(x => x.Data.TeacherIds)
                .NotNull()
                .NotEmpty()
                .WithMessage("Список преподавателей не должен быть пустым.")
                .Must(list => !list.Any(id => id < 0))
                .WithMessage("Идентификаторы преподавателей невалидны");
        }
    }
}

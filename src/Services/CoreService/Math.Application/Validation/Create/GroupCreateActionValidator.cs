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
                .MaximumLength(100).WithMessage("Название слишком длинное");
            RuleFor(x => x.Data.CourseId)
                .GreaterThan(0).WithMessage("Курс обязателен");
            // Пустой состав допустим: группу заводят заранее и наполняют позже
            RuleFor(x => x.Data.Members).ValidMembers();
        }
    }
}

using Application.Commands.UpdateCommands;
using FluentValidation;

namespace Application.Validation.Update
{
    public sealed class GroupUpdateActionValidator : AbstractValidator<UpdateGroupCommand>
    {
        public GroupUpdateActionValidator()
        {
            RuleFor(x => x.Data.Id)
                .GreaterThan(0).WithMessage("Идентификатор должен быть больше 0");
            RuleFor(x => x.Data.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
            RuleFor(x => x.Data.CourseId)
                .GreaterThan(0).WithMessage("Курс обязателен");
            RuleFor(x => x.Data.Members).ValidMembers();
        }
    }
}

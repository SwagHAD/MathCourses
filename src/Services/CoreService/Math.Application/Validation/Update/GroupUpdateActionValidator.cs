using Application.Commands.UpdateCommands;
using FluentValidation;

namespace Application.Validation.Update
{
    public sealed class GroupUpdateActionValidator : AbstractValidator<UpdateGroupCommand>
    {
        public GroupUpdateActionValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
        }
    }
}

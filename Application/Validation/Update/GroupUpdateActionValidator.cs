using Application.Commands.UpdateCommands;
using Application.Validation.Base;
using FluentValidation;

namespace Application.Validation.Update
{
    public class GroupUpdateActionValidator : BaseValidator<UpdateGroupDto>
    {
        public GroupUpdateActionValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
        }
    }
}

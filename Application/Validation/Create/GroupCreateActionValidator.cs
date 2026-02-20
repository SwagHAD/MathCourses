using Application.Commands.CreateCommands;
using Application.Validation.Base;
using FluentValidation;

namespace Application.Validation.Create
{
    public class GroupCreateActionValidator : BaseValidator<CreateGroupCommand>
    {
        public GroupCreateActionValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(50).WithMessage("Название слишком длинное");
        }
    }
}

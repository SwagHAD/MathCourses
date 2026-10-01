using Application.Commands.DeleteCommands;
using FluentValidation;

namespace Application.Validation.Delete
{
    public sealed class GroupDeleteActionValidator : AbstractValidator<DeleteGroupCommand>
    {
        public GroupDeleteActionValidator() 
        {
            RuleFor(f => f.ID)
                .GreaterThan(0)
                .WithMessage("Индификатор должен быть больше 0");
        }
    }
}

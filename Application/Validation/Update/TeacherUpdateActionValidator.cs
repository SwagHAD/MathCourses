using Application.Commands.UpdateCommands;
using Application.Validation.Base;
using FluentValidation;

namespace Application.Validation.Update
{
    public class TeacherUpdateActionValidator : BaseValidator<UpdateTeacherDto>
    {
        public TeacherUpdateActionValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
        }
    }
}

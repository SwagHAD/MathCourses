using Application.DTO.TeacherDto;
using Application.Validation.Base;
using FluentValidation;

namespace Application.Validation.Create
{
    public class TeacherCreateActionValidator : BaseValidator<CreateTeacherDto>
    {
        public TeacherCreateActionValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя обязательно")
                .MaximumLength(100).WithMessage("Имя слишком длинное");
        }
    }
}

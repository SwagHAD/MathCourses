using Application.DTO.StudentDTO;
using Application.Validation.Base;
using FluentValidation;

namespace Application.Validation.Create
{
    public class StudentCreateActionValidator : BaseValidator<CreateStudentDto>
    {
        public StudentCreateActionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Имя обязательно")
                .MaximumLength(100).WithMessage("Имя слишком длинное");
        }
    }
}

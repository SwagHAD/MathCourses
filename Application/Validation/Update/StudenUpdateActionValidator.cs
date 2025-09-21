using Application.DTO.StudentDto;
using Application.Validation.Base;
using FluentValidation;

namespace Application.Validation.Update
{
    public class StudenUpdateActionValidator : BaseValidator<UpdateStudentDto>
    {
        public StudenUpdateActionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
        }
    }
}

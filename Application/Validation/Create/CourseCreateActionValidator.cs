using Application.DTO.CourseDTO;
using Application.Validation.Base;
using FluentValidation;

namespace Application.Validation.Create
{
    public class CourseCreateActionValidator : BaseValidator<CreateCourseDto>
    {
        public CourseCreateActionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
        }
    }
}

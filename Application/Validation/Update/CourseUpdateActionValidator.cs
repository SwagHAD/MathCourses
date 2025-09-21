using Application.DTO.CourseDto;
using Application.Validation.Base;
using FluentValidation;

namespace Application.Validation.Update
{
    public class CourseUpdateActionValidator : BaseValidator<UpdateCourseDto>
    {
        public CourseUpdateActionValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
        }
    }
}

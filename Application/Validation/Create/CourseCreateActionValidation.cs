using Application.DTO.CourseDto;
using Application.Validation.Base;
using FluentValidation;

namespace Application.Validation.Create
{
    public class CourseCreateActionValidation : BaseValidator<CreateCourseDto>
    {
        public CourseCreateActionValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
        }
    }
}

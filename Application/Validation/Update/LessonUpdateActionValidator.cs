using Application.DTO.LessonDto;
using Application.Validation.Base;
using FluentValidation;

namespace Application.Validation.Update
{
    public class LessonUpdateActionValidator : BaseValidator<UpdateLessonDto>
    {
        public LessonUpdateActionValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
        }
    }
}

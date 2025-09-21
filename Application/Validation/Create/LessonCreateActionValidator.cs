using Application.DTO.LessonDto;
using Application.Validation.Base;
using Domain.Entities;
using FluentValidation;

namespace Application.Validation.Create
{
    public class LessonCreateActionValidator : BaseValidator<CreateLessonDto>
    {
        public LessonCreateActionValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(100).WithMessage("Название слишком длинное");
        }
    }
}

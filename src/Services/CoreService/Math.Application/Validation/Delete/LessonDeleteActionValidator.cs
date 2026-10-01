using Application.Commands.DeleteCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validation.Delete
{
    public sealed class LessonDeleteActionValidator : AbstractValidator<DeleteLessonCommand>
    {
        public LessonDeleteActionValidator() 
        {
            RuleFor(f => f.ID)
                .GreaterThan(0)
                .WithMessage("Индификатор должен быть больше 0");
        }
    }
}

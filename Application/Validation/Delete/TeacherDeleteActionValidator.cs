using Application.Commands.DeleteCommands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validation.Delete
{
    public sealed class TeacherDeleteActionValidator : AbstractValidator<DeleteTeacherCommand>
    {
        public TeacherDeleteActionValidator() 
        {
            RuleFor(f => f.ID)
                .GreaterThan(0)
                .WithMessage("Индификатор должен быть больше 0");
        }
    }
}

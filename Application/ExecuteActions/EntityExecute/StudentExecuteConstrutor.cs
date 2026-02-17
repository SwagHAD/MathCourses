using Application.Commands.UpdateCommands;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.ExecuteActions.EntityExecute
{
    public sealed class StudentExecuteConstrutor() : IExecuteConstructor<Student, UpdateStudentCommand>
    {
        public Expression<Func<UpdateSettersBuilder<Student>, UpdateSettersBuilder<Student>>> Execute(UpdateStudentCommand dto)
        {
            return setters => setters
                .SetProperty(s => s.Name, dto.Name);
        }
    }
}

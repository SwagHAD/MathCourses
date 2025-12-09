using Application.DTO.StudentDTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.ExecuteActions.EntityExecute
{
    public sealed class StudentExecuteConstrutor() : IExecuteConstructor<Student, UpdateStudentDto>
    {
        public Expression<Func<SetPropertyCalls<Student>, SetPropertyCalls<Student>>> Execute(UpdateStudentDto dto)
        {
            return setters => setters
                .SetProperty(s => s.Name, dto.Name);
        }
    }
}

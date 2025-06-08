using BLL.Repository;
using DataMath.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.MR.StudentMR.Queries
{
    public class GetAllStudentsDetailQueryHandler(IGenericRepository<Student> studentRepository) : IRequestHandler<GetAllStudentsDetailsQuery, ICollection<Student>>
    {
        public async Task<ICollection<Student>> Handle(GetAllStudentsDetailsQuery request, CancellationToken cancellationToken)
        {
            return await studentRepository.GetAllAsync(cancellationToken);
        }
    }
}

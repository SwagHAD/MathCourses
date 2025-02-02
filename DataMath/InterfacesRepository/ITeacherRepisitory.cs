using DataMath.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMath.InterfacesRepository
{
    public interface ITeacherRepisitory
    {
        Task CreateAsync(Teacher teacher, CancellationToken cancellationToken = default);
    }
}

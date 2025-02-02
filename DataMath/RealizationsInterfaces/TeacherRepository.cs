using DataMath.Entities;
using DataMath.InterfacesRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMath.RealizationsInterfaces
{
    internal class TeacherRepository(IMathContext ctx) : ITeacherRepisitory
    {
        public async Task CreateAsync(Teacher teacher, CancellationToken cancellationToken = default)
        {
            await ctx.Teachers.AddAsync(teacher, cancellationToken);
            await ctx.SaveChangesAsync(cancellationToken);
        }
    }
}

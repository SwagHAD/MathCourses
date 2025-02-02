using DataMath.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IGroupService
    {
        Task CreateAsync(string NameOfGroup, Teacher teacher, CancellationToken cancellationToken);
    }
}

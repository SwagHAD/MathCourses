using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MR.GroupMR.Commands.DeleteGroup
{
    public class DeleteGroupCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}

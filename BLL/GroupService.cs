using DataMath.Entities;
using DataMath.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GroupService(IGroupRepository groupRepository) : IGroupService
    {
        public async Task CreateAsync(string NameOfGroup, Teacher teacher, CancellationToken cancellationToken)
        {
            var group = new Group()
            {
                Name = NameOfGroup,
                TeacherId = teacher.Id,
                Teacher = teacher,
            };
            await groupRepository.CreateAsync(group);
        }
    }
}

using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.GroupDto
{
    public class UpdateGroupDto : IDataTransferObjectBase<Group>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

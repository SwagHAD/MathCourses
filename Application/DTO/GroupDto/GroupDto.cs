using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.GroupDto
{
    public class GroupDto : IDataTransferObjectBase<Group>
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}

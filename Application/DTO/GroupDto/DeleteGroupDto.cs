using Application.DTO.Base;
using Domain.Entities;

namespace Application.DTO.GroupDto
{
    public class DeleteGroupDto : IDataTransferObjectBase<Group>
    {
        public int Id { get; set; }
    }
}

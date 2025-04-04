using DataMath.Dto;

namespace WebServices.Services
{
    interface IGroupService
    {
        Task<GetGroupDto> GetGroupAsync(int Id);
        Task<CreateGroupDto> CreateGroupAsync(GetGroupDto newgroup);
    }
}

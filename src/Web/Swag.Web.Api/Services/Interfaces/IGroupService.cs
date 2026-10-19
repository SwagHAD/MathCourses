using CoreService.Contracts.Groups;

namespace Swag.Web.Api.Services.Interfaces
{
    /// <summary>Карточка группы — со студентами и преподавателями.</summary>
    public interface IGroupService
        : ICrudService<GroupResponse, GroupDetailsResponse, GroupFilter, CreateGroupRequest, UpdateGroupRequest>;
}

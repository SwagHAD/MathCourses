using CoreService.Contracts;
using CoreService.Contracts.Groups;
using Swag.Web.Api.Services.Interfaces;

namespace Swag.Web.Api.Services.Internal
{
    internal sealed class GroupService(HttpClient httpClient)
        : CrudService<GroupResponse, GroupDetailsResponse, GroupFilter, CreateGroupRequest, UpdateGroupRequest>(httpClient, Urls),
          IGroupService
    {
        private static readonly EntityRoutes Urls = new(
            Get: CoreRoutes.Url(CoreRoutes.Groups.Get),
            GetPage: CoreRoutes.Url(CoreRoutes.Groups.GetPage),
            Create: CoreRoutes.Url(CoreRoutes.Groups.Create),
            Update: CoreRoutes.Url(CoreRoutes.Groups.Update),
            Delete: CoreRoutes.Url(CoreRoutes.Groups.Delete));
    }
}

using CoreService.Contracts;
using CoreService.Contracts.Teachers;
using Swag.Web.Api.Services.Interfaces;

namespace Swag.Web.Api.Services.Internal
{
    internal sealed class TeacherService(HttpClient httpClient)
        : CrudService<TeacherResponse, TeacherResponse, TeacherFilter, CreateTeacherRequest, UpdateTeacherRequest>(httpClient, Urls),
          ITeacherService
    {
        private static readonly EntityRoutes Urls = new(
            Get: CoreRoutes.Url(CoreRoutes.Teachers.Get),
            GetPage: CoreRoutes.Url(CoreRoutes.Teachers.GetPage),
            Create: CoreRoutes.Url(CoreRoutes.Teachers.Create),
            Update: CoreRoutes.Url(CoreRoutes.Teachers.Update),
            Delete: CoreRoutes.Url(CoreRoutes.Teachers.Delete));
    }
}

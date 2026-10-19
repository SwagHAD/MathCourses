using CoreService.Contracts;
using CoreService.Contracts.Courses;
using Swag.Web.Api.Services.Interfaces;

namespace Swag.Web.Api.Services.Internal
{
    internal sealed class CourseService(HttpClient httpClient)
        : CrudService<CourseResponse, CourseResponse, CourseFilter, CreateCourseRequest, UpdateCourseRequest>(httpClient, Urls),
          ICourseService
    {
        private static readonly EntityRoutes Urls = new(
            Get: CoreRoutes.Url(CoreRoutes.Courses.Get),
            GetPage: CoreRoutes.Url(CoreRoutes.Courses.GetPage),
            Create: CoreRoutes.Url(CoreRoutes.Courses.Create),
            Update: CoreRoutes.Url(CoreRoutes.Courses.Update),
            Delete: CoreRoutes.Url(CoreRoutes.Courses.Delete));
    }
}

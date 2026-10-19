using CoreService.Contracts;
using CoreService.Contracts.Students;
using Swag.Web.Api.Services.Interfaces;

namespace Swag.Web.Api.Services.Internal
{
    internal sealed class StudentService(HttpClient httpClient)
        : CrudService<StudentResponse, StudentResponse, StudentFilter, CreateStudentRequest, UpdateStudentRequest>(httpClient, Urls),
          IStudentService
    {
        private static readonly EntityRoutes Urls = new(
            Get: CoreRoutes.Url(CoreRoutes.Students.Get),
            GetPage: CoreRoutes.Url(CoreRoutes.Students.GetPage),
            Create: CoreRoutes.Url(CoreRoutes.Students.Create),
            Update: CoreRoutes.Url(CoreRoutes.Students.Update),
            Delete: CoreRoutes.Url(CoreRoutes.Students.Delete));
    }
}

using System.Net.Http.Json;
using CoreService.Contracts;
using CoreService.Contracts.Courses;
using CoreService.Contracts.Groups;
using CoreService.Contracts.Students;
using CoreService.Contracts.Teachers;
using SharedKernel.Contracts.Pagination;

namespace Swag.Web.Api
{
    /// <summary>Запросы к CoreService. Страница и количество по фильтру приходят одним ответом.</summary>
    public sealed class CoreApi(IHttpClientFactory httpClientFactory)
    {
        public Task<PageResult<StudentResponse>> GetStudentsAsync(PageRequest<StudentFilter> request,
            CancellationToken cancellationToken = default) =>
            GetPageAsync<StudentResponse, StudentFilter>(MathRoutes.Students.GetPage, request, cancellationToken);

        public Task<PageResult<TeacherResponse>> GetTeachersAsync(PageRequest<TeacherFilter> request,
            CancellationToken cancellationToken = default) =>
            GetPageAsync<TeacherResponse, TeacherFilter>(MathRoutes.Teachers.GetPage, request, cancellationToken);

        public Task<PageResult<GroupResponse>> GetGroupsAsync(PageRequest<GroupFilter> request,
            CancellationToken cancellationToken = default) =>
            GetPageAsync<GroupResponse, GroupFilter>(MathRoutes.Groups.GetPage, request, cancellationToken);

        public Task<PageResult<CourseResponse>> GetCoursesAsync(PageRequest<CourseFilter> request,
            CancellationToken cancellationToken = default) =>
            GetPageAsync<CourseResponse, CourseFilter>(MathRoutes.Courses.GetPage, request, cancellationToken);

        private async Task<PageResult<TItem>> GetPageAsync<TItem, TFilter>(string action, PageRequest<TFilter> request,
            CancellationToken cancellationToken)
            where TFilter : class, IQueryFilter, new()
        {
            using var client = httpClientFactory.CreateClient(HttpClients.Core);
            var url = QueryString.ForPage(MathRoutes.Url(action), request);
            return await client.GetFromJsonAsync<PageResult<TItem>>(url, cancellationToken) ?? new PageResult<TItem>([], 0);
        }
    }
}

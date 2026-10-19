using CoreService.Contracts.Courses;

namespace Swag.Web.Api.Services.Interfaces
{
    public interface ICourseService
        : ICrudService<CourseResponse, CourseResponse, CourseFilter, CreateCourseRequest, UpdateCourseRequest>;
}

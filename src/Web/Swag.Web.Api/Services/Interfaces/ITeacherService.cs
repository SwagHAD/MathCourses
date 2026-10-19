using CoreService.Contracts.Teachers;

namespace Swag.Web.Api.Services.Interfaces
{
    public interface ITeacherService
        : ICrudService<TeacherResponse, TeacherResponse, TeacherFilter, CreateTeacherRequest, UpdateTeacherRequest>;
}

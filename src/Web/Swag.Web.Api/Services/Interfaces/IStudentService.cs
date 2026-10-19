using CoreService.Contracts.Students;

namespace Swag.Web.Api.Services.Interfaces
{
    public interface IStudentService
        : ICrudService<StudentResponse, StudentResponse, StudentFilter, CreateStudentRequest, UpdateStudentRequest>;
}

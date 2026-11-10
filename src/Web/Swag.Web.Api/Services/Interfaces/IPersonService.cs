using CoreService.Contracts.Persons;

namespace Swag.Web.Api.Services.Interfaces
{
    /// <summary>Люди. Студент или преподаватель — роль в группе, фильтр <see cref="PersonFilter.Kind"/>.</summary>
    public interface IPersonService
        : ICrudService<PersonResponse, PersonDetailsResponse, PersonFilter, CreatePersonRequest, UpdatePersonRequest>;
}

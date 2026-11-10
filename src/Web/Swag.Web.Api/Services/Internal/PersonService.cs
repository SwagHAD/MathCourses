using CoreService.Contracts;
using CoreService.Contracts.Persons;
using Swag.Web.Api.Services.Interfaces;

namespace Swag.Web.Api.Services.Internal
{
    internal sealed class PersonService(HttpClient httpClient)
        : CrudService<PersonResponse, PersonDetailsResponse, PersonFilter, CreatePersonRequest, UpdatePersonRequest>(httpClient, Urls),
          IPersonService
    {
        private static readonly EntityRoutes Urls = new(
            Get: CoreRoutes.Url(CoreRoutes.Persons.Get),
            GetPage: CoreRoutes.Url(CoreRoutes.Persons.GetPage),
            Create: CoreRoutes.Url(CoreRoutes.Persons.Create),
            Update: CoreRoutes.Url(CoreRoutes.Persons.Update),
            Delete: CoreRoutes.Url(CoreRoutes.Persons.Delete));
    }
}

using Microsoft.AspNetCore.Mvc;
using SharedKernel.Entities;

namespace Math.Api.Controllers
{
    public abstract class BaseSwagController<T> : ControllerBase
        where T : BaseEntity
    {
    }
}

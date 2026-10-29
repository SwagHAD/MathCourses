using SharedKernel.Application.Interfaces;
using SharedKernel.Application.Queries;

namespace Application.Queries.Base
{
    public interface IResoursePermissionQuery<TRequest> : IQuery<TRequest>
    {
        Task<bool> CheckResoursePermissionAsync(int userid, ISwagDbContext db, CancellationToken cancellationToken);
    }
}

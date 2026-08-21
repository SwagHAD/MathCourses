using Application.Interfaces;
using Domain.Enums;

namespace Application.Queries.Base
{
    public interface IResoursePermissionQuery<TRequest> : IQuery<TRequest>
    {
        Task<bool> CheckResoursePermissionAsync(int userid, UserType userType, ISwagDbContext db, CancellationToken cancellationToken);
    }
}

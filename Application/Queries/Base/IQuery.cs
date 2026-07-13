using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Queries.Base
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
        Task<bool> CheckResoursePermissionAsync(int userid, UserType userType, ISwagDbContext db, CancellationToken cancellationToken)
            => Task.FromResult(true);
    }
}

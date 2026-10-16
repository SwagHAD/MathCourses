using Application.Queries.DefaultQueries;
using AuthService.Contracts.Users;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.Select
{
    public sealed class GetUserHandler(ISwagDbContext DbContext) : IRequestHandler<GetUserQuery, UserResponse>
    {
        public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await DbContext.Set<User>().AsNoTracking()
                .Select(x => new UserResponse
                {
                    Id = x.Id,
                    Login = x.Login,
                    RoleId = x.RoleId
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(nameof(User), request.Id);
        }
    }
}

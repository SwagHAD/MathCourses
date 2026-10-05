using Application.Queries.DefaultQueries;
using Application.Responses.DefaultResponses;
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
                    Role = x.RoleId ?? 0
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(nameof(User), request.Id);
        }
    }
}

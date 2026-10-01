using Application.Interfaces;
using Application.Queries.DefaultQueries;
using Application.Responses.DefaultResponses;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Select
{
    public sealed class GetUserHandler(IAuthDbContext DbContext) : IRequestHandler<GetUserQuery, UserResponse>
    {
        public async Task<UserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await DbContext.Users.AsNoTracking()
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

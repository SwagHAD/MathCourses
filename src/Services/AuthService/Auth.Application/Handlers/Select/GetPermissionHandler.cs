using Application.Interfaces;
using Application.Queries.DefaultQueries;
using Application.Responses.DefaultResponses;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Select
{
    public sealed class GetPermissionHandler(IAuthDbContext DbContext) : IRequestHandler<GetPermissionQuery, PermissionResponse>
    {
        public async Task<PermissionResponse> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            return await DbContext.Permissions.AsNoTracking()
                .Select(x => new PermissionResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Permission), request.Id);
        }
    }
}

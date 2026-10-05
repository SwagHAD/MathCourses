using Application.Queries.DefaultQueries;
using Application.Responses.DefaultResponses;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.Select
{
    public sealed class GetRoleHandler(ISwagDbContext DbContext) : IRequestHandler<GetRoleQuery, RoleResponse>
    {
        public async Task<RoleResponse> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            return await DbContext.Set<Role>().AsNoTracking()
                .Select(x => new RoleResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    PermissionIds = x.RolePermissions.Select(rp => rp.PermissionId).ToArray()
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Role), request.Id);
        }
    }
}

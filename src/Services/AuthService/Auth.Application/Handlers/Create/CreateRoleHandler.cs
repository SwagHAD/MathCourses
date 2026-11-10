using Application.Commands.Create;
using Domain.Entities;
using MediatR;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.Create
{
    public sealed class CreateRoleHandler(ISwagDbContext DbContext) : IRequestHandler<CreateRoleCommand, int>
    {
        public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var newrole = new Role
            {
                Name = request.Data.Name,
                RolePermissions = request.Data.Permissions
                    .Select(grant => new RolePermission { PermissionId = grant.PermissionId, Scope = grant.Scope })
                    .ToList()
            };
            await DbContext.Set<Role>().AddAsync(newrole, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return newrole.Id;
        }
    }
}

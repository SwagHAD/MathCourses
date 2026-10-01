using Application.Commands.Create;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.Create
{
    public sealed class CreateRoleHandler(IAuthDbContext DbContext) : IRequestHandler<CreateRoleCommand, int>
    {
        public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var newrole = new Role
            {
                Name = request.Name,
                RolePermissions = request.PermissionIds.Select(id => new RolePermission { PermissionId = id }).ToList()
            };
            await DbContext.Roles.AddAsync(newrole, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return newrole.Id;
        }
    }
}

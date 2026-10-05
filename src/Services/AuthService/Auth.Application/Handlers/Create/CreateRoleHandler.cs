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
                Name = request.Name,
                RolePermissions = request.PermissionIds.Select(id => new RolePermission { PermissionId = id }).ToList()
            };
            await DbContext.Set<Role>().AddAsync(newrole, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            return newrole.Id;
        }
    }
}

using Application.Queries.DefaultQueries;
using Application.Responses.DefaultResponses;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.Select
{
    public sealed class GetPermissionHandler(ISwagDbContext DbContext) : IRequestHandler<GetPermissionQuery, PermissionResponse>
    {
        public async Task<PermissionResponse> Handle(GetPermissionQuery request, CancellationToken cancellationToken)
        {
            return await DbContext.Set<Permission>().AsNoTracking()
                .Select(x => new PermissionResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken) ?? throw new NotFoundException(nameof(Permission), request.Id);
        }
    }
}

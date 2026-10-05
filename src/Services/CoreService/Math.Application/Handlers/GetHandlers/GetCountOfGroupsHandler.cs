using Application.Queries.DefaultQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using Domain.Entities;

namespace Application.Handlers.GetHandlers
{
    internal sealed class GetCountOfGroupsHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetCountOfGroupsQuery, int>
    {
        public async Task<int> Handle(GetCountOfGroupsQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Set<Group>().CountAsync(cancellationToken);
        }
    }
}

using Application.Interfaces;
using Application.Queries.DefaultQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.GetHandlers
{
    internal sealed class GetCountOfGroupsHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetCountOfGroupsQuery, int>
    {
        public async Task<int> Handle(GetCountOfGroupsQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Groups.CountAsync(cancellationToken);
        }
    }
}

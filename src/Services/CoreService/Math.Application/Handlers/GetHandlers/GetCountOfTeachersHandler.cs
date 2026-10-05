using Application.Queries.DefaultQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using Domain.Entities;

namespace Application.Handlers.GetHandlers
{
    internal sealed class GetCountOfTeachersHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetCountOfTeachersQuery, int>
    {
        public async Task<int> Handle(GetCountOfTeachersQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Set<Teacher>().CountAsync(cancellationToken);
        }
    }
}

using Application.Interfaces;
using Application.Queries.DefaultQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.GetHandlers
{
    internal sealed class GetCountOfTeachersHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetCountOfTeachersQuery, int>
    {
        public async Task<int> Handle(GetCountOfTeachersQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Teachers.CountAsync(cancellationToken);
        }
    }
}

using Application.Interfaces;
using Application.Queries.DefaultQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.GetHandlers
{
    internal sealed class GetCountOfStudentsHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetCountOfStudentsQuery, int>
    {
        public async Task<int> Handle(GetCountOfStudentsQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Students.CountAsync(cancellationToken);
        }
    }
}

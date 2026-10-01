using Application.Interfaces;
using Application.Queries.DefaultQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.GetHandlers
{
    internal sealed class GetCountOfCoursesHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetCountOfCoursesQuery, int>
    {
        public async Task<int> Handle(GetCountOfCoursesQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Courses.CountAsync(cancellationToken);
        }
    }
}

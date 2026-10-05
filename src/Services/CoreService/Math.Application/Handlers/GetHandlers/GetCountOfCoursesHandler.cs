using Application.Queries.DefaultQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using Domain.Entities;

namespace Application.Handlers.GetHandlers
{
    internal sealed class GetCountOfCoursesHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetCountOfCoursesQuery, int>
    {
        public async Task<int> Handle(GetCountOfCoursesQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Set<Course>().CountAsync(cancellationToken);
        }
    }
}

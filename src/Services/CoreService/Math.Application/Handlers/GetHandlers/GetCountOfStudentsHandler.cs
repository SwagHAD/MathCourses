using Application.Queries.DefaultQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using Domain.Entities;

namespace Application.Handlers.GetHandlers
{
    internal sealed class GetCountOfStudentsHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetCountOfStudentsQuery, int>
    {
        public async Task<int> Handle(GetCountOfStudentsQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Set<Student>().CountAsync(cancellationToken);
        }
    }
}

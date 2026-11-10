using Application.Queries.DefaultQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using Domain.Entities;

namespace Application.Handlers.GetHandlers
{
    internal sealed class GetCountOfPersonsHandler(ISwagDbContext dbContext) : IRequestHandler<GetCountOfPersonsQuery, int>
    {
        public async Task<int> Handle(GetCountOfPersonsQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.Set<Person>().CountAsync(cancellationToken);
        }
    }
}

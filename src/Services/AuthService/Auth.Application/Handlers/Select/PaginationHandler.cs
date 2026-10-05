using Application.Queries.Base;
using SharedKernel.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.Select
{
    public sealed class PaginationHandler<T>(ISwagDbContext DbContext) : IRequestHandler<PaginationQuery<T>, T[]> where T : BaseEntity
    {
        public async Task<T[]> Handle(PaginationQuery<T> request, CancellationToken cancellationToken)
        {
            var query = DbContext.Set<T>().AsQueryable();
            query = request.ApplyFilter(query);
            return await query
                .Skip((request.Page - 1) * request.Count)
                .Take(request.Count)
                .ToArrayAsync(cancellationToken);
        }
    }
}

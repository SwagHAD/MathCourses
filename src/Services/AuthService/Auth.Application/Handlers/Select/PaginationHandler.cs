using Application.Interfaces;
using Application.Queries.Base;
using Domain.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.Select
{
    public sealed class PaginationHandler<T>(IAuthDbContext DbContext) : IRequestHandler<PaginationQuery<T>, T[]> where T : BaseEntity
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

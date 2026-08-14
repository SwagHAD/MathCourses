using Application.Interfaces;
using Application.Queries.PaginationQueries;
using Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Handlers.GetHandlers
{
    public sealed class GetStudentsHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetStudentsPaginationQuery, DefaultStudentResponse[]>
    {
        public async Task<DefaultStudentResponse[]> Handle(GetStudentsPaginationQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Set<Student>()
                .Where(f => f.Name.Contains(request.NameSearch ?? string.Empty))
                .Skip(request.Count * (request.PageIndex - 1))
                .Take(request.Count)
                .Select(f => new DefaultStudentResponse
                {
                    Id = f.ID,
                    Name = f.Name
                })
                .ToArrayAsync();
        }
    }
}

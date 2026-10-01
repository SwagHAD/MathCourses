using Application.Interfaces;
using Application.Queries.DefaultQueries;
using Application.Responses;
using Application.Tools;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Handlers.GetHandlers
{
    public sealed class GetStudentHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetStudentQuery, DefaultStudentResponse>
    {
        public async Task<DefaultStudentResponse> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Students.AsNoTracking().Where(f => f.ID == request.Id).Select(s => new DefaultStudentResponse
            {
                Id = s.ID,
                Name = s.Name,
            }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Student).GetDescription(), request.Id);
        }
    }
}

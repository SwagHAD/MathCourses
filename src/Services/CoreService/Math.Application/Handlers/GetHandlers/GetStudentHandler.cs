using Application.Queries.DefaultQueries;
using Application.Responses;
using SharedKernel.Tools;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.GetHandlers
{
    public sealed class GetStudentHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetStudentQuery, DefaultStudentResponse>
    {
        public async Task<DefaultStudentResponse> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Set<Student>().AsNoTracking().Where(f => f.ID == request.Id).Select(s => new DefaultStudentResponse
            {
                Id = s.ID,
                Name = s.Name,
            }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Student).GetDescription(), request.Id);
        }
    }
}

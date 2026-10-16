using Application.Queries.DefaultQueries;
using CoreService.Contracts.Students;
using SharedKernel.Tools;
using Domain.Entities;
using SharedKernel.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;

namespace Application.Handlers.GetHandlers
{
    public sealed class GetStudentHandler(ISwagDbContext swagDbContext) : IRequestHandler<GetStudentQuery, StudentResponse>
    {
        public async Task<StudentResponse> Handle(GetStudentQuery request, CancellationToken cancellationToken)
        {
            return await swagDbContext.Set<Student>().AsNoTracking().Where(f => f.ID == request.Id).Select(s => new StudentResponse
            {
                Id = s.ID,
                Name = s.Name,
            }).FirstOrDefaultAsync(cancellationToken) ?? throw new NotFoundException(typeof(Student).GetDescription(), request.Id);
        }
    }
}

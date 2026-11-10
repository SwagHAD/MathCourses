using Application.Queries.DefaultQueries;
using CoreService.Contracts.Persons;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Interfaces;
using SharedKernel.Exceptions;
using SharedKernel.Tools;

namespace Application.Handlers.GetHandlers
{
    public sealed class GetPersonHandler(ISwagDbContext dbContext) : IRequestHandler<GetPersonQuery, PersonDetailsResponse>
    {
        public async Task<PersonDetailsResponse> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.Set<Person>().AsNoTracking().Where(p => p.Id == request.Id)
                .Select(p => new PersonDetailsResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Surname = p.Surname,
                    Patronymic = p.Patronymic,
                    FullName = p.FullName,
                    Age = p.Age,
                    Email = p.Email,
                    Phone = p.Phone,
                    UserId = p.UserId,
                    Groups = p.Memberships.Select(m => new PersonGroupResponse
                    {
                        GroupId = m.GroupId,
                        GroupName = m.GroupRef.Name,
                        Kind = m.Kind,
                        Status = m.Status
                    }).ToArray()
                }).FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException(typeof(Person).GetDescription(), request.Id);
        }
    }
}

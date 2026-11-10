using CoreService.Contracts.Persons;
using SharedKernel.Application.Queries;

namespace Application.Queries.DefaultQueries
{
    public sealed record GetPersonQuery : IQuery<PersonDetailsResponse>
    {
        public long Id { get; init; }
    }
}

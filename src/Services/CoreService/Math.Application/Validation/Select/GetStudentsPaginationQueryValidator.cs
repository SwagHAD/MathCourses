using Application.Queries.PaginationQueries;
using FluentValidation;

namespace Application.Validation.Select
{
    public sealed class GetStudentsPaginationQueryValidator : AbstractValidator<GetStudentsPaginationQuery>
    {
        public GetStudentsPaginationQueryValidator()
        {
            RuleFor(f => f.Count)
                .GreaterThan(0).WithMessage("Количество должно быть больше 0");
            RuleFor(f => f.PageIndex)
                .GreaterThan(0).WithMessage("Номер страницы должен быть больше 0");
        }
    }
}

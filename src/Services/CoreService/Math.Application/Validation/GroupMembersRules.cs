using CoreService.Contracts.Groups;
using FluentValidation;

namespace Application.Validation
{
    /// <summary>Общие правила состава группы для создания и изменения.</summary>
    internal static class GroupMembersRules
    {
        public static IRuleBuilderOptions<T, IReadOnlyList<GroupMemberRequest>> ValidMembers<T>(
            this IRuleBuilder<T, IReadOnlyList<GroupMemberRequest>> rule) =>
            rule
                .NotNull()
                .Must(members => members.All(m => m.PersonId > 0))
                .WithMessage("Идентификаторы участников невалидны")
                .Must(members => members.Select(m => m.PersonId).Distinct().Count() == members.Count)
                .WithMessage("Человек не может быть в группе дважды")
                .Must(members => members.All(m => Enum.IsDefined(m.Kind) && Enum.IsDefined(m.Status)))
                .WithMessage("Неизвестная роль или статус участника");
    }
}

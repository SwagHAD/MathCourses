using CoreService.Contracts.Groups;
using Domain.Access;
using Domain.Entities;

namespace Application.Access.Projections
{
    /// <summary>
    /// Активный участник группы → group:{GroupId}#learner|instructor@person:{PersonId}.
    /// Отчисленные и выпустившиеся в граф не попадают: доступ к материалам группы у них пропадает,
    /// а сама запись участия остаётся в истории.
    /// </summary>
    internal sealed class GroupMembershipProjection : AccessTupleProjection<GroupMembership>
    {
        public override IReadOnlyCollection<OwnedRelation> OwnedRelations { get; } =
        [
            new(AccessObjectType.Group, AccessRelations.Learner),
            new(AccessObjectType.Group, AccessRelations.Instructor),
        ];

        protected override IEnumerable<TupleKey> Project(GroupMembership membership)
        {
            if (membership.Status != MembershipStatus.Active)
                yield break;

            yield return TupleKey.Direct(
                AccessObjectType.Group, membership.GroupId, RelationOf(membership.Kind),
                AccessObjectType.Person, membership.PersonId);
        }

        protected override IQueryable<GroupMembership> Source(IQueryable<GroupMembership> query) =>
            query.Where(m => m.Status == MembershipStatus.Active);

        private static string RelationOf(MembershipKind kind) => kind switch
        {
            MembershipKind.Learner => AccessRelations.Learner,
            MembershipKind.Instructor => AccessRelations.Instructor,
            _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, "Неизвестная роль в группе")
        };
    }
}

using CoreService.Contracts.Groups;
using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    /// <summary>
    /// Участие человека в группе — заменяет StudentGroup/TeacherGroup.
    /// Источник истины для кортежей доступа group#learner / group#instructor.
    /// Кортежи пересчитываются в SaveChanges, поэтому ExecuteUpdate/ExecuteDelete по этой таблице
    /// (и каскад из БД без загрузки участников) оставят граф несогласованным — менять только через трекер.
    /// </summary>
    [Title("Участник группы", Secured = true)]
    public sealed class GroupMembership : BaseEntity
    {
        public long GroupId { get; set; }
        public Group GroupRef { get; set; } = null!;
        public long PersonId { get; set; }
        public Person PersonRef { get; set; } = null!;
        public MembershipKind Kind { get; set; }
        public MembershipStatus Status { get; set; }
        public DateTimeOffset JoinedAt { get; set; }
        public DateTimeOffset? LeftAt { get; set; }
    }
}

using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    /// <summary>
    /// Человек. Студент он или преподаватель — не свойство человека, а его роль в конкретной группе
    /// (<see cref="GroupMembership.Kind"/>).
    /// </summary>
    [Title("Персона", Secured = true)]
    public sealed class Person : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Surname { get; set; }
        public string? Patronymic { get; set; }
        public string FullName { get; set; } = null!;
        public short Age { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        /// <summary>Учётная запись в AuthService (там ключи int).</summary>
        public int? UserId { get; set; }

        public List<GroupMembership> Memberships { get; set; } = new();

        /// <summary>Хранится отдельной колонкой, чтобы искать и сортировать по ФИО одним индексом.</summary>
        public void RefreshFullName() =>
            FullName = string.Join(' ', new[] { Surname, Name, Patronymic }.Where(part => !string.IsNullOrWhiteSpace(part)));
    }
}

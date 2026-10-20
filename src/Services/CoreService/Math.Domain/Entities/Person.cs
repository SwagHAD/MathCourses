using SharedKernel.Attributes;
using SharedKernel.Entities;

namespace Domain.Entities
{
    [Title("Персона", Secured = true)]
    public sealed class Person : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string FullName { get; set; }
        public short Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? UserId { get; set; }
    }
}

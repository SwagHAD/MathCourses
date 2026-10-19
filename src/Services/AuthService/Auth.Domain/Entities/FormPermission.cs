using SharedKernel.Entities;
using SharedKernel.Enums;

namespace Domain.Entities
{
    public sealed class FormPermission : BaseEntity
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public Form FormRef { get; set; }
        public ActionType ActionType { get; set; }
    }
}

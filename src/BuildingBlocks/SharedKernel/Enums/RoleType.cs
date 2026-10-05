using SharedKernel.Attributes;

namespace SharedKernel.Enums
{
    public enum RoleType
    {
        [Title("Админ")]
        Admin = 0,
        [Title("Студент")]
        Student = 1,
        [Title("Преподаватель")]
        Teacher = 2,
    }
}

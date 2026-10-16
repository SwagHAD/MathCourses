namespace Swag.Web.Auth
{
    /// <summary>Роль приходит в токене числом — значения как в SharedKernel.Enums.RoleType.</summary>
    public static class RoleNames
    {
        public const string Admin = "0";
        public const string Student = "1";
        public const string Teacher = "2";

        public static string Describe(string? role) => role switch
        {
            Admin => "Админ",
            Student => "Студент",
            Teacher => "Преподаватель",
            _ => "Неизвестная роль"
        };
    }
}

namespace AuthService.Contracts
{
    /// <summary>Маршруты AuthService: контроллеры используют их в атрибутах, клиент — для URL.</summary>
    public static class AuthRoutes
    {
        public const string Prefix = "api/auth";

        public static string Url(string action) => $"{Prefix}/{action}";

        public static class Auth
        {
            public const string Login = "login";
            public const string Refresh = "refresh";
            public const string Logout = "logout";
        }

        public static class Users
        {
            public const string Get = "GetUser";
            public const string GetPage = "GetUsers";
            public const string CurrentPermissions = "GetUserPermissions";
        }

        public static class Roles
        {
            public const string Get = "GetRole";
            public const string GetPage = "GetRoles";
            public const string Create = "CreateRole";
            public const string Update = "UpdateRole";
            public const string Delete = "DeleteRole";
        }

        public static class Permissions
        {
            public const string Get = "GetPermission";
            public const string GetPage = "GetPermissions";
        }
    }
}

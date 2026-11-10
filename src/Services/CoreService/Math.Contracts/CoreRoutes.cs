namespace CoreService.Contracts
{
    /// <summary>Маршруты CoreService: контроллеры используют их в атрибутах, клиент — для URL.</summary>
    public static class CoreRoutes
    {
        public const string Prefix = "api/core";

        public static string Url(string action) => $"{Prefix}/{action}";

        public static class Persons
        {
            public const string Get = "GetPerson";
            public const string GetPage = "GetPersons";
            public const string Count = "GetCountOfPersons";
            public const string Create = "CreatePerson";
            public const string Update = "UpdatePerson";
            public const string Delete = "DeletePerson";
        }

        public static class Lessons
        {
            public const string Get = "GetLesson";
            public const string GetPage = "GetLessons";
            public const string Count = "GetCountOfLessons";
            public const string Create = "CreateLesson";
            public const string Update = "UpdateLesson";
            public const string Delete = "DeleteLesson";
        }

        public static class Groups
        {
            public const string Get = "GetGroup";
            public const string GetPage = "GetGroups";
            public const string Count = "GetCountOfGroups";
            public const string Create = "CreateGroup";
            public const string Update = "UpdateGroup";
            public const string Delete = "DeleteGroup";
        }

        public static class Access
        {
            /// <summary>Пересобрать выводимые кортежи доступа из доменных таблиц.</summary>
            public const string Rebuild = "RebuildAccessTuples";
        }

        public static class Courses
        {
            public const string Get = "GetCourse";
            public const string GetPage = "GetCourses";
            public const string Count = "GetCountOfCourses";
            public const string Create = "CreateCourse";
            public const string Update = "UpdateCourse";
            public const string Delete = "DeleteCourse";
        }
    }
}

namespace CoreService.Contracts
{
    /// <summary>Маршруты CoreService: контроллеры используют их в атрибутах, клиент — для URL.</summary>
    public static class MathRoutes
    {
        public const string Prefix = "api/core";

        public static string Url(string action) => $"{Prefix}/{action}";

        public static class Students
        {
            public const string Get = "GetStudent";
            public const string GetPage = "GetStudents";
            public const string Count = "GetCountOfStudents";
            public const string Create = "CreateStudent";
            public const string Update = "UpdateStudent";
            public const string Delete = "DeleteStudent";
        }

        public static class Teachers
        {
            public const string Get = "GetTeacher";
            public const string GetPage = "GetTeachers";
            public const string Count = "GetCountOfTeachers";
            public const string Create = "CreateTeacher";
            public const string Update = "UpdateTeacher";
            public const string Delete = "DeleteTeacher";
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

namespace Domain.Access
{
    /// <summary>
    /// Тип вершины графа доступа. Хранится числом (smallint) — значения не переиспользовать и не менять.
    /// Не путать с ObjectType в AuthService: тот описывает RBAC-права на тип сущности целиком.
    /// </summary>
    public enum AccessObjectType : short
    {
        Person = 1,
        Group = 2,
        Course = 3,
        Lesson = 4,
        Session = 5,
        Homework = 6,
        Submission = 7
    }
}

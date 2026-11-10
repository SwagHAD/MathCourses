namespace Domain.Access
{
    /// <summary>
    /// Отношения, которые хранятся кортежами. Вычисляемые отношения (member, viewer урока и т.п.)
    /// здесь не нужны — они собираются из этих правилами схемы.
    /// </summary>
    public static class AccessRelations
    {
        /// <summary>group:{id}#learner@person:{id} — учится в группе.</summary>
        public const string Learner = "learner";

        /// <summary>group:{id}#instructor@person:{id} — ведёт группу.</summary>
        public const string Instructor = "instructor";

        /// <summary>course:{id}#editor@person:{id} — автор/методист курса (явная выдача).</summary>
        public const string Editor = "editor";

        /// <summary>course:{id}#viewer@... — явное открытие курса вне групп (гость, проверяющий).</summary>
        public const string Viewer = "viewer";
    }
}

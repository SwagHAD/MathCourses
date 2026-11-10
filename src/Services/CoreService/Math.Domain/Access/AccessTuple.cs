using SharedKernel.Attributes;

namespace Domain.Access
{
    /// <summary>
    /// Факт графа доступа: object#relation@subject[#subjectRelation].
    /// Неизменяемый: кортеж либо есть, либо удаляется физически.
    /// Кортежи, выведенные из доменных таблиц (состав групп), пишет только AccessTupleInterceptor —
    /// руками создаются лишь явные выдачи (editor/viewer курса).
    /// </summary>
    [Title("Кортеж доступа", Secured = true)]
    public sealed class AccessTuple
    {
        public const int RelationMaxLength = 32;

        public long Id { get; private set; }

        public AccessObjectType ObjectType { get; private set; }
        public long ObjectId { get; private set; }
        public string Relation { get; private set; } = null!;

        public AccessObjectType SubjectType { get; private set; }
        public long SubjectId { get; private set; }
        /// <summary>null — конкретный субъект; иначе userset (например, group:7#learner).</summary>
        public string? SubjectRelation { get; private set; }

        /// <summary>true — факт принадлежит другому сервису и пришёл событием.</summary>
        public bool IsReplica { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        private AccessTuple() { } // EF

        private AccessTuple(TupleKey key, bool isReplica)
        {
            ObjectType = key.ObjectType;
            ObjectId = key.ObjectId;
            Relation = ValidateRelation(key.Relation, nameof(key.Relation));
            SubjectType = key.SubjectType;
            SubjectId = key.SubjectId;
            SubjectRelation = key.SubjectRelation is null ? null : ValidateRelation(key.SubjectRelation, nameof(key.SubjectRelation));
            IsReplica = isReplica;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public static AccessTuple Create(TupleKey key) => new(key, isReplica: false);

        /// <summary>Чужой факт, пришедший событием из сервиса-владельца.</summary>
        public static AccessTuple Replica(TupleKey key) => new(key, isReplica: true);

        public TupleKey Key => new(ObjectType, ObjectId, Relation, SubjectType, SubjectId, SubjectRelation);

        private static string ValidateRelation(string value, string paramName)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, paramName);
            if (value.Length > RelationMaxLength)
                throw new ArgumentException($"Relation is longer than {RelationMaxLength} chars.", paramName);
            return value;
        }

        public override string ToString() => Key.ToString();
    }
}

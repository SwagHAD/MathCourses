namespace Domain.Access
{
    /// <summary>
    /// Значение кортежа без служебных полей: object#relation@subject[#subjectRelation].
    /// Сравнивается по значению — на этом строится вычисление разницы «было/стало».
    /// </summary>
    public readonly record struct TupleKey(
        AccessObjectType ObjectType, long ObjectId, string Relation,
        AccessObjectType SubjectType, long SubjectId, string? SubjectRelation = null)
    {
        /// <summary>group:7#learner@person:42</summary>
        public static TupleKey Direct(AccessObjectType objectType, long objectId, string relation,
            AccessObjectType subjectType, long subjectId) =>
            new(objectType, objectId, relation, subjectType, subjectId);

        /// <summary>course:3#viewer@group:7#learner — все, у кого есть отношение к субъекту.</summary>
        public static TupleKey Userset(AccessObjectType objectType, long objectId, string relation,
            AccessObjectType subjectType, long subjectId, string subjectRelation) =>
            new(objectType, objectId, relation, subjectType, subjectId, subjectRelation);

        public override string ToString() =>
            $"{ObjectType}:{ObjectId}#{Relation}@{SubjectType}:{SubjectId}"
            + (SubjectRelation is null ? "" : $"#{SubjectRelation}");
    }
}

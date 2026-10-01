namespace Domain.Exceptions
{
    public sealed class PermissionDeniedException : Exception
    {
        public PermissionDeniedException(string objectName, string action)
            : base($"У вас недостаточно прав для {action} объекта \"{objectName}\"") { }
    }
}

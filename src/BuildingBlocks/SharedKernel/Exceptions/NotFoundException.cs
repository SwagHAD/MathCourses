namespace SharedKernel.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Объект \"{name}\" ({key}) не найден.") { }
    }
}

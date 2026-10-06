namespace SharedKernel.Models
{
    /// <summary>Описание типа объекта: имя класса сущности и его название из <see cref="Attributes.TitleAttribute"/>.</summary>
    public sealed record ObjectTypeDescriptor
    {
        public string Name { get; init; } = null!;
        public string Title { get; init; } = null!;
    }
}

namespace Domain.Attributes
{
    public sealed class TitleAttribute : Attribute
    {
        public string Description { get; set; } = null!;
        public TitleAttribute(string description)
        {
            Description = description;
        }
    }
}

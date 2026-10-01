namespace Domain.Attributes
{
    public sealed class TitleAttribute : Attribute
    {
        public TitleAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; set; } = null!;
    }
}

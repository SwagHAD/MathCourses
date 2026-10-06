namespace SharedKernel.Attributes
{
    public sealed class TitleAttribute : Attribute
    {
        public TitleAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; set; } = null!;
        /// <summary>
        /// Сущность — тип объекта, на который выдаются доступы.
        /// Такие сущности автоматически регистрируются в AuthService.
        /// </summary>
        public bool Secured { get; set; }
    }
}

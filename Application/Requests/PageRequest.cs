namespace Application.Requests
{
    public class PageRequest<T>
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public string SeacrhText { get; set; } = string.Empty;
    }
}

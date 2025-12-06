using Application.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Responses
{
    public sealed class PageListResponse<T>
    {
        public ResponseStatus Status { get; set; }
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public List<string>? Errors { get; set; }
        public string? SuccesMessege { get; set; }

        private PageListResponse(ResponseStatus status, List<string>? erorrs)
        {
            Errors = erorrs;
        }
        private PageListResponse(ResponseStatus status, IEnumerable<T> items, string? succesmessege)
        {
            Items = items;
            SuccesMessege = succesmessege;
        }
        public PageListResponse() { }

        public static PageListResponse<T> Ok(IEnumerable<T> items, string succesmessege) =>
            new(ResponseStatus.Ok, items, succesmessege);
        public static PageListResponse<T> Fail(List<string>? errors) =>
            new(ResponseStatus.ValidationError, errors);
        public static PageListResponse<T> NotFound(string messege) =>
            new(ResponseStatus.NotFound, new List<string> { messege });
        public static PageListResponse<T> Error(string message) =>
            new(ResponseStatus.Error, new List<string> { message });
    }
}

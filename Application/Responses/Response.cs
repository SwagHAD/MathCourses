using Application.Enums;

namespace Application.Responses
{
    public sealed class Response<T>
    {
        public ResponseStatus Status { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public string? SuccesMessege { get; set; }
        private Response(ResponseStatus status, List<string>? erorrs)
        {
            Errors = erorrs;
            Status = status;
        }
        private Response(ResponseStatus status, T? data, string? succesmessege)
        {
            Data = data;
            SuccesMessege = succesmessege;
            Status = status;
        }
        public Response() {}
        public static Response<T> Ok(T? data, string succesmessege) => 
            new(ResponseStatus.Ok, data, succesmessege);
        public static Response<T> Fail(List<string>? errors) => 
            new(ResponseStatus.ValidationError, errors);
        public static Response<T> NotFound(string messege) => 
            new(ResponseStatus.NotFound, new List<string> { messege });
        public static Response<T> Error(string message) =>
            new(ResponseStatus.Error, new List<string> { message });
    }
}

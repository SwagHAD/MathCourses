using Application.DTO.Base;
using Application.Enums;

namespace Application.Responses
{
    public class Response<T> where T : IDataTransferObjectBase
    {
        public ResponseStatus Status { get; set; }
        public T? Data { get; set; }

        public List<string>? Errors { get; set; }

        public string? SuccesMessege { get; set; }

        protected Response(ResponseStatus status)
        {
            Status = status;
        }
        protected Response(ResponseStatus status, List<string>? erorrs) : this(status)
        {
            Errors = erorrs;
        }
        protected Response(ResponseStatus status, T? data, string? succesmessege) : this(status)
        {
            Data = data;
            SuccesMessege = succesmessege;
        }
        public Response()
        {

        }
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

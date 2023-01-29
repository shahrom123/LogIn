using System.Net;

namespace Domain.Response
{
    public class Response<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }

        public Response(T data)
        {
            Data = data;
            Errors = new List<string>();
            StatusCode = 200;
        }

        public Response(HttpStatusCode statusCode, List<string> errors)
        {
            Errors = errors;
            StatusCode = (int)statusCode;
        }
    }
}

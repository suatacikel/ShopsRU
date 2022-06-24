using System.Net;
using System.Text.Json.Serialization;

namespace ShopsRU.DiscountAPI.Dtos.Response
{
    public class ResponseDto<T>
    {

        public T Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }

        public bool IsSuccessful { get; private set; }

        public List<string> Errors { get; set; }

        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> { StatusCode = statusCode, IsSuccessful = true };
        }

        public static ResponseDto<T> Error(string error, int statusCode)
        {
            return new ResponseDto<T> { StatusCode = statusCode, IsSuccessful = false, Errors = new List<string> { error } };
        }

        public static ResponseDto<T> Error(List<string> errors, int statusCode)
        {
            return new ResponseDto<T> { StatusCode = statusCode, IsSuccessful = false, Errors = errors };
        }



        //public bool Error { get; set; } = false;

        //public string Message { get; set; }

        //public object Result { get; set; }

        //public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        //public int ErrorCode { get; set; }
    }
}

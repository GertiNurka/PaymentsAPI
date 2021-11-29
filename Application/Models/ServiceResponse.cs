using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsAPI.Application.Models
{
    public class ServiceResponse<T> {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ServiceResponse(bool success, int statusCode, string message)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
        }

        public ServiceResponse(bool success, int statusCode, string message, T data)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        public ObjectResult GetObjectResult()
        {
            ObjectResult result;

            switch (StatusCode)
            {
                case StatusCodes.Status200OK:
                    result = new OkObjectResult(Data);
                    break;
                case StatusCodes.Status204NoContent:
                    result = new OkObjectResult(null);
                    break;
                case StatusCodes.Status400BadRequest:
                    result = new BadRequestObjectResult("");
                    break;
                case StatusCodes.Status401Unauthorized:
                    result = new UnauthorizedObjectResult("");
                    break;
                case StatusCodes.Status404NotFound:
                    result = new NotFoundObjectResult("");
                    break;
                case StatusCodes.Status409Conflict:
                    result = new NotFoundObjectResult("");
                    break;
                default:
                    result = new ObjectResult("");
                    break;
            }

            return result;
        }
    }
}
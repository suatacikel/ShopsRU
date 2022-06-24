using Newtonsoft.Json;
using ShopsRU.DiscountAPI.Common.Constants;
using ShopsRU.DiscountAPI.Dtos.Response;
using System.Net;

namespace ShopsRU.DiscountAPI.Middleware
{
    public class ExceptionMiddleware :  MiddlewareBase
    {
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(httpContext, ex, (int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, (int)HttpStatusCode.InternalServerError, Messages.SomethingWentWrong);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode,  string message = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;

            var result = ResponseDto<NoContentDto>.Error(message ?? exception.Message, statusCode);

            await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

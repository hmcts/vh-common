using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net;

namespace VH.Core.Middleware.Exception
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;


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
            catch (BadRequestException ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex);
            }
            catch (System.Exception ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, System.Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(exception.Message);
        }
    }
}

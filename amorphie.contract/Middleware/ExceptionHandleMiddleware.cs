using amorphie.core.Base;

namespace amorphie.contract.Middleware
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, httpContext);
            }
        }

        private async Task HandleExceptionAsync(Exception ex, HttpContext httpContext)
        {
            var statusCode = 500;
            var errorMessage = "Unknown error";

            if (ex is InvalidOperationException)
            {
                statusCode = 400;
                errorMessage = "Invalid operation";
            }
            else if (ex is ArgumentException)
            {
                statusCode = 400;
                errorMessage = "Invalid argument";
            }

            httpContext.Response.StatusCode = statusCode;


            await httpContext.Response.WriteAsJsonAsync(new Response
            {
                Result = new Result(amorphie.core.Enums.Status.Error, errorMessage)
            });

        }
    }

    public static class ExceptionHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandleMiddleware>();
        }
    }

}
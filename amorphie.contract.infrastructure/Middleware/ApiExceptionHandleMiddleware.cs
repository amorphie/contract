using System.Net;
using amorphie.contract.data.Extensions;
using amorphie.contract.data.Extensions.CustomException;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace amorphie.contract.data.Middleware
{
  public class ApiExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;
        private const string UnexpectedErrorMessage = "Beklenmeyen bir hata ile karsilasildi.";

        public ApiExceptionHandleMiddleware(RequestDelegate next)
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


            var messages = new List<string>();
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var title = "Server error";
            //TODO: COmment ds
            switch (ex)
            {
                case InvalidOperationException validationExp:
                    {
                        statusCode = (int)HttpStatusCode.BadRequest;
                        title = "InvalidOperationException Error";
                        messages.Add(validationExp.Message);
                        break;
                    }
                case ArgumentException validationExp:
                    {
                        statusCode = (int)HttpStatusCode.BadRequest;
                        title = "ArgumentException Error";
                        messages.Add(validationExp.Message);
                        break;
                    }
                case NotImplementedException validationExp:
                    {
                        statusCode = (int)HttpStatusCode.BadRequest;
                        title = "NotImplementedException Error";
                        messages.Add(validationExp.Message);
                        break;
                    }
                case CustomBaseException customBaseException:
                    {
                        statusCode = (int)customBaseException.StatusCode;
                        title = customBaseException.Title;

                        var responseExMessage = customBaseException.MessageFormat;
                        foreach (var (key, value) in customBaseException.MessageProps)
                            responseExMessage = responseExMessage.Replace(key, value);

                        messages.Add(responseExMessage);
                        break;
                    }
                default:
                    messages.Add(UnexpectedErrorMessage);
                    break;
            }
            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = string.Join(" ## ", messages.ToArray())
            };
            httpContext.Response.StatusCode = statusCode;
            Log.Error(ex, "An unhandled exception has occurred. Message: {Message} Request path: {Path}, Query string: {QueryString}",
                     problemDetails, httpContext.Request.Path, httpContext.Request.QueryString);
            var responseResult = GenericResult<GenericResponse>.Exception(problemDetails);
            await httpContext.Response.WriteAsJsonAsync(responseResult);

        }
    }

    public static class ApiExceptionHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiExceptionHandleMiddlewareExtensions(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiExceptionHandleMiddleware>();
        }
    }
}
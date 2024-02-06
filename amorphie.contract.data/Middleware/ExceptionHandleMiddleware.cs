using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;
using amorphie.core.Base;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace amorphie.contract.data.Middleware
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
                var model = new HeaderFilterModel();

                if (httpContext.Request.Headers.TryGetValue(AppHeaderConsts.BusinessLine, out var businessLine))
                {
                    model.EBankEntity = GetBankEntity(businessLine.FirstOrDefault());
                }
                else if (httpContext.Request.Headers.TryGetValue(AppHeaderConsts.Application, out var application))
                {
                    model.EBankEntity = GetBankEntity(application.FirstOrDefault());
                }
                else model.EBankEntity = EBankEntity.on;

                if (httpContext.Request.Headers.TryGetValue(AppHeaderConsts.Language, out var lang))
                    model.LangCode = lang.FirstOrDefault();
                else
                    model.LangCode = "en-EN";

                if (httpContext.Request.Headers.TryGetValue(AppHeaderConsts.ClientId, out var clientId))
                    model.ClientCode = clientId.FirstOrDefault();

                if (httpContext.Request.Headers.TryGetValue(AppHeaderConsts.UserReference, out var userReference))
                    model.UserReference = userReference.FirstOrDefault();

                httpContext.Items[AppHeaderConsts.HeaderFilterModel] = model;

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
                Result = new Result(amorphie.core.Enums.Status.Error, errorMessage, ex.Message)
            });

        }

        private EBankEntity GetBankEntity(string businessLine)
        {
            return businessLine switch
            {
                "X" => EBankEntity.on,
                "B" => EBankEntity.burgan,
                _ => throw new NotImplementedException($"{nameof(EBankEntity)} is not yet implemented.")
            };
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
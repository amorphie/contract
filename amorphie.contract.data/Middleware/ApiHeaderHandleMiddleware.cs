using amorphie.contract.core.Enum;
using amorphie.contract.core.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace amorphie.contract.data.Middleware
{
    public class ApiHeaderHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiHeaderHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            string _langCode = "";
            string _businessLine = "";
            string _clientCode = "";
            string _userReference = "";
            if (httpContext.Request.Headers.TryGetValue(AppHeaderConsts.BusinessLine, out var businessLine))
            {
                _businessLine = businessLine.FirstOrDefault();
            }
            else if (httpContext.Request.Headers.TryGetValue(AppHeaderConsts.Application, out var application))
            {
                _businessLine = application.FirstOrDefault();
            }
            // else model.EBankEntity = EBankEntity.on;//serhat kaldırttı

            if (httpContext.Request.Headers.TryGetValue(AppHeaderConsts.AcceptLanguage, out var aLang))
            {
                _langCode = aLang.FirstOrDefault();
            }
            else if (httpContext.Request.Headers.TryGetValue(AppHeaderConsts.Language, out var lang))
                _langCode = lang.FirstOrDefault();
            else _langCode = "tr-TR";

            if (httpContext.Request.Headers.TryGetValue(AppHeaderConsts.ClientId, out var clientId))
                _clientCode = clientId.FirstOrDefault();

            if (httpContext.Request.Headers.TryGetValue(AppHeaderConsts.UserReference, out var userReference))
                _userReference = userReference.FirstOrDefault();
            // else
            //     throw new ArgumentNullException($"{AppHeaderConsts.UserReference} cannot be null");
            var model = new HeaderFilterModel(_businessLine, _langCode, _clientCode, _userReference);

            httpContext.Items[AppHeaderConsts.HeaderFilterModel] = model;

            await _next(httpContext);

        }
    }

    public static class ApiHeaderHandleMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiHeaderHandleMiddlewareExtensions(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiHeaderHandleMiddleware>();
        }
    }

}
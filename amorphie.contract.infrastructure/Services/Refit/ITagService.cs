using Refit;
using amorphie.contract.Models.Proxy;

namespace amorphie.contract.infrastructure.Services.Refit
{
    public interface ITagService
    {
        [Get("/pdfTemplate/{domainName}/{entityName}/{tagName}/{viewTemplateName}/execute?reference={reference}&version={version}")]
        Task<HttpResponseMessage> RenderPdfFromTag(string domainName, string entityName, string tagName, string viewTemplateName, string reference, string version);

        [Get("/htmlTemplate/{domainName}/{entityName}/{tagName}/{viewTemplateName}/execute?reference={reference}&version={version}")]
        Task<HttpResponseMessage> RenderHtmlFromTag(string domainName, string entityName, string tagName, string viewTemplateName, string reference, string version);
        
        [Get("/tag/{domainName}/{entityName}/{tagName}/execute?reference={reference}")]
        Task<HttpResponseMessage> GetRenderValuesFromTag(string domainName, string entityName, string tagName, string reference);
    }
}
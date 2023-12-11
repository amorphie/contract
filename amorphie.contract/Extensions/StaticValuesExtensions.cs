using System;
namespace amorphie.contract.Extensions
{
	public static class StaticValuesExtensions
	{
        public static string TemplateEngineUrl { get; set; }
        public static string TemplateEngineHtmlRenderEndpoint { get; set; }
        public static string TemplateEnginePdfRenderEndpoint { get; set; }

        public static void SetStaticValues(AppSettings settings)
        {
            TemplateEngineUrl = settings.TemplateEngine.Url;
            TemplateEngineHtmlRenderEndpoint = settings.TemplateEngine.HtmlRenderEndpoint;
            TemplateEnginePdfRenderEndpoint = settings.TemplateEngine.PdfRenderEndpoint;
        }
    }
}


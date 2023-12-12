using System;
namespace amorphie.contract.Extensions
{
	public class AppSettings
	{
		public TemplateEngine TemplateEngine { get; set; }
	}

	public class TemplateEngine
	{
		public string Url { get; set; }
		public string HtmlRenderEndpoint { get; set; }
		public string PdfRenderEndpoint { get; set; }
		public string GetTemplateEndpoint { get; set; }
	}
}


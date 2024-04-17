using amorphie.contract.application.TemplateEngine;

namespace amorphie.contract.application.ConverterFactory;

public class TemplateRenderConverter : IFileContentProvider
{
    private readonly ITemplateEngineAppService _templateEngineAppService;

    public TemplateRenderConverter(ITemplateEngineAppService templateEngineAppService)
    {
        _templateEngineAppService = templateEngineAppService;
    }
    public async Task<byte[]> GetFileContentAsync(string fileContext)
    {
        var content = await _templateEngineAppService.GetRenderPdf(fileContext);
        return Convert.FromBase64String(content.Data);
    }
    public string GetName()
    {
        return AppConsts.ConverterTemplateRender;
    }
}

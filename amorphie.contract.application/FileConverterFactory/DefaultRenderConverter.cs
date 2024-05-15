namespace amorphie.contract.application.ConverterFactory;

public class DefaultRenderConverter : IFileContentProvider
{
    public Task<byte[]> GetFileContentAsync(string fileContext)
    {
        return Task.FromResult(Convert.FromBase64String(fileContext));
    }
    public string GetName()
    {
        return AppConsts.ConverterDefaultRender;
    }
}

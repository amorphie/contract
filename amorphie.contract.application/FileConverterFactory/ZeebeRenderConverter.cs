namespace amorphie.contract.application.ConverterFactory;

public class ZeebeRenderConverter : IFileContentProvider
{
    public Task<byte[]> GetFileContentAsync(string fileContext)
    {
        return Task.FromResult(Convert.FromBase64String(fileContext)); //TODO: SubFlow için düzenle
    }

    public string GetName()
    {
        return AppConsts.ConverterZeebeRender;
    }
}

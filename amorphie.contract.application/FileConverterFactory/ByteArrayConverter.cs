namespace amorphie.contract.application.ConverterFactory;

public class ByteArrayConverter : IFileContentProvider
{
    public Task<byte[]> GetFileContentAsync(string fileContext)
    {
        var bytes = fileContext.Split(',').Select(byte.Parse).ToArray();
        return Task.FromResult(bytes);
    }

    public string GetName()
    {
        return  AppConsts.ConverterByteRender;
    }
}

namespace amorphie.contract.application.ConverterFactory;

public class ByteArrayConverter : IFileContentProvider
{
    public Task<byte[]> GetFileContentAsync(string fileContext)
    {
        var bytes = fileContext.Split(',').Select(byte.Parse).ToArray();
        return Task.FromResult(bytes);
    }

    public IEnumerable<string> GetNames()
    {
        return new[] { AppConsts.ConverterByteRender };
    }
}

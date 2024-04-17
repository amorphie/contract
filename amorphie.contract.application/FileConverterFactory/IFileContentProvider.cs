namespace amorphie.contract.application.ConverterFactory;

public interface IFileContentProvider
{
    Task<byte[]> GetFileContentAsync(string fileContext);

    string GetName();
}

namespace amorphie.contract.application.ConverterFactory;

public class FileConverterFactory
{
    private readonly IDictionary<string, IFileContentProvider> _converters;

    public FileConverterFactory(IEnumerable<IFileContentProvider> converters)
    {
        _converters = converters.ToDictionary(c => c.GetName(), c => c);
    }
    public IFileContentProvider GetConverter(string fileContextType)
    {
        if (!String.IsNullOrEmpty(fileContextType) && fileContextType.Contains(" "))
            fileContextType = fileContextType.Trim().Replace(" ", "");

        if (!_converters.TryGetValue(fileContextType, out var converter))
        {
            throw new NotSupportedException($"Desteklenmeyen dosya türü: {fileContextType}");
        }

        return converter;
    }
}

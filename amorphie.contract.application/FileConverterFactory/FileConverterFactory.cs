namespace amorphie.contract.application.ConverterFactory;

public class FileConverterFactory
{
    private readonly IDictionary<string, IFileContentProvider> _converters;

    public FileConverterFactory(IEnumerable<IFileContentProvider> converters)
    {
        _converters = converters
           .SelectMany(c => c.GetNames().Select(name => new { Name = name, Converter = c }))
           .ToDictionary(x => x.Name, x => x.Converter);
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

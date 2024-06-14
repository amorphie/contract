namespace amorphie.contract.core.Extensions;

public static class FileExtension
{
    private static IDictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {

        {"application/pdf", ".pdf"},
    };

    public static string GetFileExtensionFromMimeType(string mimeType)
    {
        return _mappings[mimeType] ?? throw new InvalidDataException($"{mimeType} not found");
    }


}

using amorphie.contract.core.Extensions;
using iText.Html2pdf;

namespace amorphie.contract.application.ConverterFactory;

public class HtmlToPdfConverter : IFileContentProvider
{
    public Task<byte[]> GetFileContentAsync(string fileContext)
    {
        byte[] fileBytes = Convert.FromBase64String(fileContext);
        using (MemoryStream inputStream = new MemoryStream(fileBytes))
        using (MemoryStream outputStream = new MemoryStream())
        {
            HtmlConverter.ConvertToPdf(inputStream, outputStream);
            return Task.FromResult(outputStream.ToArray());
        }
    }

    public IEnumerable<string> GetNames()
    {
        return new[] { FileExtension.Html };
    }
}


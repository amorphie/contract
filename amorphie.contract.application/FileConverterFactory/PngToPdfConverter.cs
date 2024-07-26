using amorphie.contract.core.Extensions;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace amorphie.contract.application.ConverterFactory;

public class PngToPdfConverter : IFileContentProvider
{
    public Task<byte[]> GetFileContentAsync(string fileContext)
    {
        byte[] imageFileBytes = Convert.FromBase64String(fileContext);

        using (MemoryStream outputStream = new MemoryStream())
        {
            ImageData imageData = ImageDataFactory.CreatePng(imageFileBytes);
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(outputStream));
            Document document = new Document(pdfDocument);

            Image image = new Image(imageData);
            image.SetWidth(pdfDocument.GetDefaultPageSize().GetWidth() - 50);
            image.SetAutoScaleHeight(true);

            document.Add(image);
            document.Close();

            return Task.FromResult(outputStream.ToArray());
        }
    }


    public IEnumerable<string> GetNames()
    {
        return new[] { FileExtension.Png };
    }
}


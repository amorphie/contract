using amorphie.contract.core.Extensions;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace amorphie.contract.application.ConverterFactory;

/// <summary>
/// pjpeg, jpg, x-png, gif
/// </summary>

public class OtherImageToPdfConverter : IFileContentProvider
{
    public Task<byte[]> GetFileContentAsync(string fileContext)
    {
        byte[] imageFileBytes = Convert.FromBase64String(fileContext);

        using (MemoryStream outputStream = new MemoryStream())
        {
            ImageData imageData = ImageDataFactory.Create(imageFileBytes);
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

    // DYS migration kapsamında gelen dosyaların uzantıları ve byte halleri arasında tutarsızlık olduğu için direkt ImageDataFactory de toplandı.
    public IEnumerable<string> GetNames()
    {
        return new[] { FileExtension.Jpg, FileExtension.XPng, FileExtension.PJpeg, FileExtension.Gif, FileExtension.Png };
    }
}



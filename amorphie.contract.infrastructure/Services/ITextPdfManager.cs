using amorphie.contract.core.Services;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace amorphie.contract.infrastructure.Services
{
    public class ITextPdfManager : IPdfManager
    {
        public bool VerifyPdfContent(string originalContent, string signedContent)
        {
            string originalExtractedContent = ExtractTextFromBase64(originalContent);
            string signedExtractedContent = ExtractTextFromBase64(signedContent);

            return originalExtractedContent == signedExtractedContent ? true : false;

        }


        string ExtractText(string pdfPath)
        {
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(pdfPath));
            StringWriter output = new StringWriter();
            for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
            {
                output.WriteLine(PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i)));
            }
            pdfDoc.Close();
            return output.ToString();
        }

        string ExtractTextFromBase64(string base64Pdf)
        {
            byte[] pdfBytes = Convert.FromBase64String(base64Pdf);

            using MemoryStream ms = new(pdfBytes);

            PdfDocument pdfDoc = new PdfDocument(new PdfReader(ms));

            StringWriter output = new StringWriter();
            for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
            {
                PdfPage page = pdfDoc.GetPage(i);
                // ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                string text = PdfTextExtractor.GetTextFromPage(page);
                output.WriteLine(text);
            }

            return output.ToString();
        }
    }
}
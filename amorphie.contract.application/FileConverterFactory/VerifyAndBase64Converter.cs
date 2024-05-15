// using amorphie.contract.application.TemplateEngine;
// using iText.Kernel.Pdf;
// using iText.Kernel.Pdf.Canvas.Parser;

// namespace amorphie.contract.application.ConverterFactory;

// public class VerifyAndBase64Converter : IFileContentProvider
// {
//     private readonly ITemplateEngineAppService _templateEngineAppService;

//     public VerifyAndBase64Converter(ITemplateEngineAppService templateEngineAppService)
//     {
//         _templateEngineAppService = templateEngineAppService;
//     }

//     public async Task<byte[]> GetFileContentAsync(string fileContext)
//     {
//         var originalPdfContent = await _templateEngineAppService.GetRenderPdf(fileContext);
//         if (!originalPdfContent.IsSuccess)
//         {
//             throw new Exception($"Failed to get template service {originalPdfContent.ErrorMessage}");
//         }


//         Convert.FromBase64String(content.Data);

//         return Task.FromResult(Convert.FromBase64String(fileContext));
//     }

//     public string GetName()
//     {
//         return AppConsts.VerifyAndBase64Render;
//     }

//     bool VerifyPdfContent(string originalContent, string signedContent)
//     {
//         string originalExtractedContent = ExtractText(originalContent);
//         string signedExtractedContent = ExtractText(signedContent);

//         return originalExtractedContent == signedExtractedContent ? true : false;
//     }

//     string ExtractText(string pdfPath)
//     {
//         PdfDocument pdfDoc = new PdfDocument(new PdfReader(pdfPath));
//         StringWriter output = new StringWriter();
//         for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
//         {
//             output.WriteLine(PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i)));
//         }
//         pdfDoc.Close();
//         return output.ToString();
//     }
// }

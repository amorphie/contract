namespace amorphie.contract.core.Extensions;

public static class FileExtension
{

    #region  MimeTypes
    public const string Jpeg = "image/jpeg";
    public const string PJpeg = "image/pjpeg";
    public const string Tiff = "image/tiff";
    public const string Png = "image/png";
    public const string Bmp = "image/bmp";
    public const string Pdf = "application/pdf";
    public const string Doc = "application/msword";
    public const string Docx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
    public const string Xls = "application/vnd.ms-excel";
    public const string Xlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    public const string Html = "text/html";
    public const string OctetStream = "application/octet-stream";

    // pjpeg, jpg, x-png, gif
    public const string Jpg = "image/jpg";
    public const string XPng = "image/x-png";
    public const string Gif = "image/gif";

    #endregion

    private static IDictionary<string, string> _mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) {

        {Pdf, ".pdf"},
        {Html, ".html"},
    };

    public static string GetFileExtensionFromMimeType(string mimeType)
    {
        return _mappings[mimeType] ?? throw new InvalidDataException($"{mimeType} not found");
    }


}

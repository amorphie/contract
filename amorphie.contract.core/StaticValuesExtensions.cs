namespace amorphie.contract.core
{
    public class AppSettings
    {
        public Minio minio { get; set; }
        public TemplateEngine TemplateEngine { get; set; }
        public Dms Dms { get; set; }
        public Fora Fora { get; set; }

    }
    public class TemplateEngine
    {
        public string Url { get; set; }
        public string HtmlRenderEndpoint { get; set; }
        public string PdfRenderEndpoint { get; set; }
        public string GetTemplateEndpoint { get; set; }
        public string RenderInstance { get; set; }

    }
    public class Minio
    {

        public string Url { get; set; }
        public string BucketName { get; set; }
        public string EndPoint { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }

    public class Dms
    {
        public string Url { get; set; }
    }

    public class Fora
    {
        public string ColleteralUrl { get; set; }
        public string UserCode { get; set; }

    }

    public static class StaticValuesExtensions
    {
        public static string DmsUrl { get; set; }


        public static string TemplateEngineUrl { get; set; }
        public static string TemplateEngineHtmlRenderEndpoint { get; set; }
        public static string TemplateEnginePdfRenderEndpoint { get; set; }
        public static string TemplateEngineGetTemplateEndpoint { get; set; }
        public static string TemplateEngineRenderInstance { get; set; }


        public static string MinioUrl { get; set; }
        public static string MinioBucketName { get; set; }
        public static string MinioEndPoint { get; set; }
        public static string AccessKey { get; set; }
        public static string SecretKey { get; set; }

        public static Fora Fora { get; set; }

        public static void SetStaticValues(AppSettings settings)
        {
            DmsUrl = settings.Dms.Url;

            TemplateEngineUrl = settings.TemplateEngine.Url;
            TemplateEngineHtmlRenderEndpoint = settings.TemplateEngine.HtmlRenderEndpoint;
            TemplateEnginePdfRenderEndpoint = settings.TemplateEngine.PdfRenderEndpoint;
            TemplateEngineGetTemplateEndpoint = settings.TemplateEngine.GetTemplateEndpoint;
            TemplateEngineRenderInstance = settings.TemplateEngine.RenderInstance;

            MinioUrl = settings.minio.Url;
            MinioBucketName = settings.minio.BucketName;
            MinioEndPoint = settings.minio.EndPoint;
            AccessKey = settings.minio.AccessKey;
            SecretKey = settings.minio.SecretKey;

            Fora = new Fora
            {
                ColleteralUrl = settings.Fora.ColleteralUrl,
                UserCode = settings.Fora.UserCode = settings.Fora.UserCode
            };
        }
    }
}
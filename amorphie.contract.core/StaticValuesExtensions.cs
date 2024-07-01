using amorphie.contract.core.Entity.Common;

namespace amorphie.contract.core
{
    public class AppSettings
    {
        public Minio minio { get; set; }
        public TemplateEngine TemplateEngine { get; set; }
        public Dms Dms { get; set; }
        public Fora Fora { get; set; }
        public Apisix Apisix { get; set; }
        public Pusula Pusula { get; set; }
        public Tag Tag { get; set; }
    }

    public class Tag {
        public string Url { get; set;}
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

    public class Apisix
    {
        public string BaseUrl { get; set; }
        public string DownloadEndpoint { get; set; }

    }

    public class Pusula
    {
        public string CustomerServicesUrl { get; set; }
    }
    public static class StaticValuesExtensions
    {
        public static string DmsUrl { get; private set; }
        public static string TemplateEngineUrl { get; private set; }
        public static string TemplateEngineHtmlRenderEndpoint { get; private set; }
        public static string TemplateEnginePdfRenderEndpoint { get; private set; }
        public static string TemplateEngineGetTemplateEndpoint { get; private set; }
        public static string TemplateEngineRenderInstance { get; private set; }
        public static string TagUrl { get; private set; }


        public static string MinioUrl { get; private set; }
        public static string MinioBucketName { get; private set; }
        public static string MinioEndPoint { get; private set; }
        public static string AccessKey { get; private set; }
        public static string SecretKey { get; private set; }

        public static Fora Fora { get; private set; }

        public static Apisix Apisix { get; private set; }

        public static Pusula Pusula { get; private set; }

        public static void SetStaticValues(AppSettings settings)
        {
            DmsUrl = settings.Dms.Url;
            TagUrl = settings.Tag.Url;

            TemplateEngineUrl = settings.TemplateEngine.Url;
            TemplateEngineHtmlRenderEndpoint = settings.TemplateEngine.HtmlRenderEndpoint;
            TemplateEnginePdfRenderEndpoint = settings.TemplateEngine.PdfRenderEndpoint;
            TemplateEngineGetTemplateEndpoint = settings.TemplateEngine.GetTemplateEndpoint;
            TemplateEngineRenderInstance = settings.TemplateEngine.RenderInstance;

            MinioBucketName = settings.minio.BucketName;
            MinioEndPoint = settings.minio.EndPoint;
            AccessKey = settings.minio.AccessKey;
            SecretKey = settings.minio.SecretKey;

            Fora = new Fora
            {
                ColleteralUrl = settings.Fora.ColleteralUrl,
                UserCode = settings.Fora.UserCode = settings.Fora.UserCode
            };

            Apisix = new Apisix
            {
                BaseUrl = settings.Apisix.BaseUrl,
                DownloadEndpoint = settings.Apisix.DownloadEndpoint
            };

            Pusula = new Pusula
            {
                CustomerServicesUrl = settings.Pusula.CustomerServicesUrl
            };
        }
    }
}
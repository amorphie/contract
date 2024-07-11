using amorphie.contract.core.Extensions;

namespace amorphie.contract.application
{

    public static class AppConsts
    {
        #region Contract Status
        public static readonly string NotValid = "not-valid";

        #endregion

        #region File Converter Factory

        public static readonly string ConverterDefaultRender = "DefaultRender";
        public static readonly string ConverterTemplateRender = "TemplateRender";
        public static readonly string ConverterByteRender = "ByteRender";
        public static readonly string ConverterZeebeRender = "ZeebeRender";
        public static readonly string VerifyAndBase64Render = "VerifyAndBase64Render";

        #endregion

        #region Content Types

        public static readonly string[] AllowedContentTypes = [FileExtension.Pdf];

        #endregion

        #region Global Processing

        public static readonly string NotStarted = "notStarted";
        public static readonly string InProgress = "inProgress";
        public static readonly string Completed = "completed";
        public static readonly string Canceled = "canceled";
        public static readonly string Failed = "failed";

        #endregion

    }
}
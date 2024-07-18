using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.core.Extensions
{
    public static class MinioExtension
    {
        public static string? DocumentDownloadMinioUrl(string? documentContentId)
        {
           return documentContentId == null ? null : $"{core.StaticValuesExtensions.Apisix.BaseUrl}{core.StaticValuesExtensions.Apisix.DownloadEndpoint}?ObjectId={documentContentId}";
        }
    }
}
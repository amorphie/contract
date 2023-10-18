using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.zeebe.Extensions
{
    public static class StaticValuesExtensions
    {
        public static string MinioUrl { get; set; }
        public static string MinioBucketName { get; set; }
        public static string MinioEndPoint { get; set; }
        public static string AccessKey { get; set; }
        public static string SecretKey { get; set; }
        public static void SetStaticValues(AppSettings settings)
        {
            MinioUrl = settings.minio.Url;
            MinioBucketName = settings.minio.BucketName;
            MinioEndPoint = settings.minio.EndPoint;
            AccessKey = settings.minio.AccessKey;
            SecretKey = settings.minio.SecretKey;
        }
    }
}
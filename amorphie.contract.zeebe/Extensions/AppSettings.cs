using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amorphie.contract.zeebe.Extensions
{
    public class AppSettings
    {
        public Minio minio { get; set; }
    }
    public class Minio
    {

        public string Url { get; set; }
        public string BucketName { get; set; }
        public string EndPoint { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}
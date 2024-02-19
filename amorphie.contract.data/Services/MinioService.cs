using amorphie.contract.core;
using amorphie.contract.core.Services;
using Dapr.Client;
using Minio;
using Minio.DataModel.Args;
using Microsoft.Extensions.Configuration;
using amorphie.contract.core.Enum;
using amorphie.contract.data.Extensions;
using Minio.DataModel;

namespace amorphie.contract.data.Services
{
    public class MinioService : IMinioService
    {
        private readonly int TTLSeconds = 120;
        private readonly string BucketName = "contract-management";

        private IMinioClient minioClient;
        private readonly DaprClient _daprClient;
        private readonly IConfiguration _configuration;
        public MinioService(DaprClient daprClient, IConfiguration configuration)
        {
            _daprClient = daprClient;
            _configuration = configuration;

            ////TODO: :vault dan al 
            var endpoint = StaticValuesExtensions.MinioEndPoint;
            var accessKey = StaticValuesExtensions.AccessKey;
            var secretKey = StaticValuesExtensions.SecretKey;
            BucketName = StaticValuesExtensions.MinioBucketName;

            minioClient = new MinioClient()
                           .WithEndpoint(endpoint)
                           .WithCredentials(accessKey, secretKey)
                           .WithSSL(true)
                           .Build();

        }
        public async Task UploadFile(byte[] data, string objectName, string contentType, string customMetadata)
        {
            MemoryStream stream = new MemoryStream(data);
            var headers = new Dictionary<string, string>
                {
                    { "x-amz-meta-custom-metadata", "customMetadata" }
                };

            var putObjectArgs = new PutObjectArgs()
                             .WithBucket(BucketName)
                             .WithObject(objectName)

                             // .WithFileName(filePath)
                             .WithStreamData(stream)
                             .WithObjectSize(stream.Length)
                             .WithContentType(contentType)
                             .WithHeaders(headers);

            await minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
            Console.WriteLine("Successfully uploaded " + objectName);
        }
        private async Task<bool> IsBucketExist(string bucketName)
        {
            var args = new BucketExistsArgs()
                .WithBucket(bucketName);

            return await minioClient.BucketExistsAsync(args);
        }
        private async Task CreateBucket(string bucketName)
        {
            var args = new MakeBucketArgs()
                .WithBucket(bucketName);

            await minioClient.MakeBucketAsync(args);
        }

        public async Task UploadFile()
        {
            try
            {

                bool isExist = await IsBucketExist(BucketName);
                Console.WriteLine(isExist);

                if (!isExist)
                    await CreateBucket(BucketName);

                byte[] data = System.Text.Encoding.UTF8.GetBytes("hello world");
                MemoryStream stream = new MemoryStream(data);

                var objectName = "golden-oldies.txt";
                var putObjectArgs = new PutObjectArgs()
                                 .WithBucket(BucketName)
                                 .WithObject(objectName)

                                 // .WithFileName(filePath)
                                 .WithStreamData(stream)
                                 .WithObjectSize(stream.Length)
                                 .WithContentType("application/octet-stream");




                await minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
                Console.WriteLine("Successfully uploaded " + objectName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<string> GetDocumentUrl(string objectName, CancellationToken token)
        {
            string storeName = _configuration[AppEnvConst.DaprStateStoreName];

            var cachedPresignedUrl = await _daprClient.CacheGetOrSetAsync<string>(async () =>
            {
                var expiry = TimeSpan.FromHours(1);
                PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                                         .WithBucket(BucketName)
                                         .WithObject(objectName)
                                         .WithExpiry((int)expiry.TotalSeconds);//TODO: Güvenlik Sorgula

                // Tek kullanımlık URL oluştur
                return await minioClient.PresignedGetObjectAsync(args);

            }, storeName, objectName, TTLSeconds);

            return cachedPresignedUrl;
        }

        public async Task UploadFile(byte[] data, string objectName, string contentType)
        {
            MemoryStream stream = new MemoryStream(data);
            var headers = new Dictionary<string, string>
                {
                    { "x-amz-meta-custom-metadata", "customMetadata" }
                };

            var putObjectArgs = new PutObjectArgs()
                             .WithBucket(BucketName)
                             .WithObject(objectName)

                             // .WithFileName(filePath)
                             .WithStreamData(stream)
                             .WithObjectSize(stream.Length)
                             .WithContentType(contentType)
                             .WithHeaders(headers);




            await minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
            Console.WriteLine("Successfully uploaded " + objectName);
        }

        public async Task<ReleaseableFileStreamModel> DownloadFile(string objectName, CancellationToken cancellationToken)
        {

            bool found = await IsBucketExist(BucketName);
            if (!found)
            {
                throw new FileNotFoundException($"Bucket '{BucketName}' not found.");
            }

            var statArgs = new StatObjectArgs()
            .WithObject(objectName)
            .WithBucket(BucketName);
            var stat = await minioClient.StatObjectAsync(statArgs, cancellationToken);

            var res = new ReleaseableFileStreamModel
            {
                ContentType = stat.ContentType,
                FileName = objectName,
            };

            var getArgs = new GetObjectArgs()
                .WithObject(objectName)
                .WithBucket(BucketName)
                .WithCallbackStream(res.SetStreamAsync);

            await res.HandleAsync(minioClient.GetObjectAsync(getArgs, cancellationToken));

            return res;

        }
    }
}
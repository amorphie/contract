using amorphie.contract.core;
using amorphie.contract.core.Services;
using Minio;
using Minio.DataModel.Args;

namespace amorphie.contract.data.Services
{
    public class MinioService : IMinioService
    {
        private IMinioClient minioClient;
        private string bucketName = "contract-management";
        public MinioService()
        {
            ////TODO: :vault dan al 
            var endpoint = StaticValuesExtensions.MinioEndPoint;
            var accessKey = StaticValuesExtensions.AccessKey;
            var secretKey = StaticValuesExtensions.SecretKey;
            bucketName = StaticValuesExtensions.MinioBucketName;

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
                             .WithBucket(bucketName)
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

                bool isExist = await IsBucketExist(bucketName);
                Console.WriteLine(isExist);

                if (!isExist)
                    await CreateBucket(bucketName);

                byte[] data = System.Text.Encoding.UTF8.GetBytes("hello world");
                MemoryStream stream = new MemoryStream(data);

                var objectName = "golden-oldies.txt";
                var putObjectArgs = new PutObjectArgs()
                                 .WithBucket(bucketName)
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
            var expiry = TimeSpan.FromHours(1);
            PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                                     .WithBucket(bucketName)
                                     .WithObject(objectName)
                                     .WithExpiry((int)expiry.TotalSeconds);//TODO: Güvenlik Sorgula

            // Tek kullanımlık URL oluştur
            var presignedUrl = await minioClient.PresignedGetObjectAsync(args);
            return presignedUrl;
        }

        public async Task UploadFile(byte[] data, string objectName, string contentType)
        {
            MemoryStream stream = new MemoryStream(data);
            var headers = new Dictionary<string, string>
                {
                    { "x-amz-meta-custom-metadata", "customMetadata" }
                };

            var putObjectArgs = new PutObjectArgs()
                             .WithBucket(bucketName)
                             .WithObject(objectName)

                             // .WithFileName(filePath)
                             .WithStreamData(stream)
                             .WithObjectSize(stream.Length)
                             .WithContentType(contentType)
                             .WithHeaders(headers);




            await minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
            Console.WriteLine("Successfully uploaded " + objectName);
        }
    }

}
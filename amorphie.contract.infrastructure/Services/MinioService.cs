using amorphie.contract.core;
using amorphie.contract.core.Services;
using Dapr.Client;
using Minio;
using Minio.DataModel.Args;
using Microsoft.Extensions.Configuration;
using amorphie.contract.core.Enum;
using amorphie.contract.infrastructure.Extensions;
using Minio.DataModel;
using amorphie.contract.core.Model.Minio;
using Minio.DataModel.Tags;
using Microsoft.AspNetCore.Http;

namespace amorphie.contract.infrastructure.Services
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
        public async Task UploadFile(UploadFileModel uploadFileModel)
        {
            bool isExist = await IsBucketExist(BucketName);

            if (!isExist)
                await CreateBucket(BucketName);

            var putObjectArgs = new PutObjectArgs()
                             .WithBucket(BucketName)
                             .WithObject(uploadFileModel.ObjectName)
                             .WithStreamData(uploadFileModel.MemoryStream)
                             .WithObjectSize(uploadFileModel.MemoryStream.Length)
                             .WithContentType(uploadFileModel.ContentType)
                             .WithHeaders(uploadFileModel.MetaDataHeader);

            await minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
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



        public async Task<GetMinioObjectModel> DownloadFile(string objectName, CancellationToken cancellationToken)
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


            using (MemoryStream destination = new())
            {

                var getArgs = new GetObjectArgs()
                    .WithObject(objectName)
                    .WithBucket(BucketName)
                    .WithCallbackStream((stream) =>
                                                {
                                                    stream.CopyTo(destination);
                                                });


                var getObj = await minioClient.GetObjectAsync(getArgs, cancellationToken);
                destination.Seek(0, SeekOrigin.Begin);

                var data = Convert.ToBase64String(destination.ToArray());

                var res = new GetMinioObjectModel
                {
                    ContentType = stat.ContentType,
                    FileName = objectName,
                    FileContent = data
                };

                return res;
            }
        }
    }
}
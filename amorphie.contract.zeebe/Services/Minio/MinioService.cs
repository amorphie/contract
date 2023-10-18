using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using amorphie.contract.zeebe.Services.Interfaces;
using amorphie.contract.zeebe.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Minio.DataModel.Encryption;
using Minio.DataModel.Result;
using Minio.DataModel.Tags;
using Minio.Exceptions;
namespace amorphie.contract.zeebe.Service.Minio
{
    public class MinioService:  IMinioService
    {
        private IMinioClient minioClient;
        private string bucketName = "contract-management";
        public MinioService()
        {
            //todo:vault dan al 
            var endpoint = StaticValuesExtensions.MinioEndPoint;
            var accessKey = StaticValuesExtensions.AccessKey;
            var secretKey = StaticValuesExtensions.SecretKey;
            bucketName= StaticValuesExtensions.MinioBucketName;

            minioClient = new MinioClient()
                           .WithEndpoint(endpoint)
                           .WithCredentials(accessKey, secretKey)
                           .WithSSL(true)
                           .Build();

        }
        public async Task UploadFile(byte[] data,string objectName ,string contentType)
        {
            MemoryStream stream = new MemoryStream(data);

            var putObjectArgs = new PutObjectArgs()
                             .WithBucket(bucketName)
                             .WithObject(objectName)

                             // .WithFileName(filePath)
                             .WithStreamData(stream)
                             .WithObjectSize(stream.Length)
                             .WithContentType(contentType);




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
            catch (Exception e) {
                Console.WriteLine(e.Message);
             }
        }

        
    }
}
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NexusProject.API.Helpers
{
    public class FileStorage : IFileStorage
    {
        private readonly string _bucketName;
        private readonly IAmazonS3 _s3Client;

        // Constructor that retrieves configuration values correctly
        public FileStorage(IConfiguration configuration)
        {
            // Correctly retrieve AWS configuration section
            var awsConfig = configuration.GetSection("AWS");

            // Ensure that these settings exist, if not throw exceptions
            if (awsConfig == null)
                throw new ArgumentNullException("AWS configuration section is missing in the appsettings.json.");

            _bucketName = awsConfig["BucketName"] ?? throw new ArgumentNullException("BucketName is missing in configuration");
            string accessKey = awsConfig["AccessKey"] ?? throw new ArgumentNullException("AccessKey is missing in configuration");
            string secretKey = awsConfig["SecretKey"] ?? throw new ArgumentNullException("SecretKey is missing in configuration");
            string region = awsConfig["Region"] ?? throw new ArgumentNullException("Region is missing in configuration");

            // Initialize the S3 client with credentials and region
            _s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
        }

        public async Task RemoveFileAsync(string path, string containerName)
        {
            var fileName = Path.GetFileName(path);

            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName
            };

            await _s3Client.DeleteObjectAsync(deleteRequest);
            Console.WriteLine($"Archivo {fileName} eliminado exitosamente del bucket {_bucketName}.");
        }

        public async Task<string> SaveFileAsync(byte[] content, string extension, string containerName)
        {
            var fileName = $"{Guid.NewGuid()}{extension}";

            using var memoryStream = new MemoryStream(content);
            var uploadRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName,
                InputStream = memoryStream,
                ContentType = "application/octet-stream",
                CannedACL = S3CannedACL.PublicRead // Configura el archivo como público (opcional)
            };

            await _s3Client.PutObjectAsync(uploadRequest);
            Console.WriteLine($"Archivo {fileName} subido exitosamente al bucket {_bucketName}.");

            // Retorna la URL pública del archivo
            return $"https://{_bucketName}.s3.{RegionEndpoint.GetBySystemName(_s3Client.Config.RegionEndpoint.SystemName).SystemName}.amazonaws.com/{fileName}";
        }
    }
}
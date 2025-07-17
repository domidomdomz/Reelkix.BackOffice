using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Options;
using Reelkix.BackOffice.Application.Common.Interfaces.Storage;

namespace Reelkix.BackOffice.Infrastructure.Storage
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3;
        private readonly StorageOptions _options;

        public S3Service(IOptions<StorageOptions> options)
        {
            _options = options.Value;

            // Retrieve environment variables or throw an exception if not set
            var awsAccessKeyId = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            var awsSecretAccessKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
            if (string.IsNullOrEmpty(awsAccessKeyId) || string.IsNullOrEmpty(awsSecretAccessKey))
            {
                throw new Exception("AWS credentials are not set in the environment variables.");
            }

            // Create the S3 client with explicit credentials and region
            var credentials = new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey);
            var region = _options.Region ?? "ap-southeast-1";
            var regionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region);
            _s3 = new AmazonS3Client(credentials, regionEndpoint);
        }

        public async Task UploadFileAsync(Stream stream, string key, string contentType)
        {
            var request = new Amazon.S3.Model.PutObjectRequest
            {
                BucketName = _options.BucketName,
                Key = key,
                InputStream = stream,
                ContentType = contentType
            };
            await _s3.PutObjectAsync(request);
        }

        public string GetPublicUrl(string key)
        {
            // Construct the public URL for the file
            return $"https://{_options.BucketName}.s3.{_options.Region}.amazonaws.com/{key}";
        }

    }
}

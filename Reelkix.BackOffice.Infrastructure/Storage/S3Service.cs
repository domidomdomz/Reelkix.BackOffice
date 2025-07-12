using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Reelkix.BackOffice.Application.Common.Interfaces.Storage;

namespace Reelkix.BackOffice.Infrastructure.Storage
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _s3;
        private readonly StorageOptions _options;

        public S3Service(IAmazonS3 s3, IOptions<StorageOptions> options)
        {
            _s3 = s3;
            _options = options.Value;
        }

        public async Task UploadFileAsync(Stream stream, string key, string contentType)
        {
            var request = new Amazon.S3.Model.PutObjectRequest
            {
                BucketName = _options.BucketName,
                Key = key,
                InputStream = stream,
                ContentType = contentType,
                CannedACL = Amazon.S3.S3CannedACL.PublicRead // Make the file publicly readable
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

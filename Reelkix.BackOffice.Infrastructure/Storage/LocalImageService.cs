using Microsoft.Extensions.Configuration;
using Reelkix.BackOffice.Application.Common.Interfaces.Storage;

namespace Reelkix.BackOffice.Infrastructure.Storage
{
    public class LocalImageService : IS3Service
    {
        private readonly string _basePath;
        private readonly string _baseUrl;

        public LocalImageService(IConfiguration config)
        {
            _basePath = config["Storage:LocalPath"] ?? "UploadedFiles";
            _baseUrl = config["BaseUrl"] ?? "https://localhost:7037/";
            Directory.CreateDirectory(_basePath);
        }

        public async Task UploadFileAsync(Stream stream, string key, string contentType)
        {
            var fullPath = Path.Combine(_basePath, key.Replace('/', Path.DirectorySeparatorChar));
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

            using var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
            await stream.CopyToAsync(fileStream);
        }

        public string GetPublicUrl(string key)
        {
            return $"{_baseUrl}static/{key.Replace('\\', '/')}";
        }
    }
}

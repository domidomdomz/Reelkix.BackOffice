namespace Reelkix.BackOffice.Application.Common.Interfaces.Storage
{
    public interface IS3Service
    {
        Task UploadFileAsync(Stream stream, string key, string contentType);
        string GetPublicUrl(string key);

        // Later: Task DeleteFileAsync(string key);
    }
}

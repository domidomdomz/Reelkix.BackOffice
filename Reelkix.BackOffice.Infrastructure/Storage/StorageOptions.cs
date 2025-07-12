namespace Reelkix.BackOffice.Infrastructure.Storage
{
    public class StorageOptions
    {
        public string BucketName { get; set; } = default!; // The name of the storage bucket where files will be stored, cannot be null or empty.
        public string Region { get; set; } = "ap-southeast-1"; // The AWS region where the storage bucket is located, cannot be null or empty.
        public string AccessKeyId { get; set; } = default!; // The access key ID for authentication with the storage service, cannot be null or empty.
        public string SecretAccessKey { get; set; } = default!; // The secret access key for authentication with the storage service, cannot be null or empty.
    }
}

using Reelkix.BackOffice.Application.Common.Interfaces.Storage;
using Reelkix.BackOffice.Domain.Products;
using Reelkix.BackOffice.Persistence.Data;

namespace Reelkix.BackOffice.Application.ProductImages.Commands.UploadProductImage
{
    public class UploadProductImageHandler
    {
        private readonly IS3Service _s3Service;
        private readonly ApplicationDbContext _db;

        public UploadProductImageHandler(IS3Service s3Service, ApplicationDbContext db)
        {
            _s3Service = s3Service;
            _db = db;
        }

        public async Task<Guid> Handle(UploadProductImageCommand command, CancellationToken cancellationToken)
        {
            // Validate the request
            if (command.FileStream == null || command.FileStream.Length == 0)
            {
                throw new ArgumentException("File stream cannot be null or empty.", nameof(command.FileStream));
            }
            if (string.IsNullOrWhiteSpace(command.FileName))
            {
                throw new ArgumentException("File name cannot be null or empty.", nameof(command.FileName));
            }

            var productImageId = Guid.NewGuid();
            var extension = Path.GetExtension(command.FileName);
            var fileName = $"{productImageId}{extension}";
            var s3Key = $"products/{command.ProductId}/{fileName}";

            // Upload the file to S3
            await _s3Service.UploadFileAsync(command.FileStream, s3Key, command.ContentType);
            
            // Generate the public URL for the uploaded file
            var publicUrl = _s3Service.GetPublicUrl(s3Key);
            
            // Create a new ProductImage entity
            var productImage = new ProductImage(
                id: productImageId,
                productId: command.ProductId, 
                url: publicUrl, 
                altText: command.AltText, 
                sortOrder: command.SortOrder);

            // Save the image metadata to the database
            _db.ProductImages.Add(productImage);
            await _db.SaveChangesAsync(cancellationToken);
            return productImage.Id;
        }

    }
}

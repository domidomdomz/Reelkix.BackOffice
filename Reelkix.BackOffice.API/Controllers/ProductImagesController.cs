using Microsoft.AspNetCore.Mvc;
using Reelkix.BackOffice.Application.ProductImages.Commands.UploadProductImage;

namespace Reelkix.BackOffice.API.Controllers
{
    [Route("api/products/{productId}/images")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly UploadProductImageHandler _uploadProductImageHandler;
        public ProductImagesController(UploadProductImageHandler uploadProductImageHandler)
        {
            _uploadProductImageHandler = uploadProductImageHandler;
        }

        [HttpPost]
        // Single or batched full image uploads
        public async Task<IActionResult> UploadImage(
            Guid productId, 
            [FromForm] int sortOrder, 
            [FromForm] IFormFile file, 
            CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File cannot be null or empty.");
            if (productId == Guid.Empty)
                return BadRequest("Invalid product ID.");

            var command = new UploadProductImageCommand
            {
                ProductId = productId,
                FileStream = file.OpenReadStream(),
                FileName = file.FileName,
                ContentType = file.ContentType,
                AltText = Path.GetFileNameWithoutExtension(file.FileName), // You might want to set this to something more meaningful
                SortOrder = sortOrder
            };

            var imageId = await _uploadProductImageHandler.Handle(command, cancellationToken);
            if (imageId == Guid.Empty)
                return BadRequest("Failed to upload image.");

            return Created(string.Empty, new { imageId });
        }

        [HttpPost]
        // For chunked uploads
        public async Task<IActionResult> UploadChunkedImage([FromForm] UploadChunkCommand command, CancellationToken cancellationToken)
        {
            // Here you would typically handle the chunked upload logic, such as saving the chunk to a temporary location
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetImages(Guid productId, CancellationToken cancellation)
        {
            if (productId == Guid.Empty)
                return BadRequest("Invalid product ID.");
            // Here you would typically retrieve the images from a storage service or database
            // For demonstration, we will return a placeholder response
            var images = new List<string>
            {
                "https://example.com/image1.jpg",
                "https://example.com/image2.jpg"
            };
            return Ok(images);
        }
    }
}

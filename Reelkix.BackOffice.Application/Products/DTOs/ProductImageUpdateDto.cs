namespace Reelkix.BackOffice.Application.Products.DTOs
{
    public class ProductImageUpdateDto
    {
        public Guid ImageId { get; set; } = Guid.Empty; // The unique identifier of the product image to be updated, defaulting to an empty GUID.
        public int SortOrder { get; set; } = 0; // The order in which the image should be displayed, defaulting to 0.
        public string AltText { get; set; } = string.Empty; // The alternative text for the image, cannot be null or empty.
    }
}

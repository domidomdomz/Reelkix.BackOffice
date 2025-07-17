namespace Reelkix.BackOffice.Application.ProductImages.Commands.DeleteProductImage
{
    public class DeleteProductImageCommand
    {
        public Guid ProductId { get; set; }
        public Guid ProductImageId { get; set; }
    }
}

namespace Reelkix.BackOffice.Application.Products.Commands.CreateDraftProduct
{
    public class CreateDraftProductCommand
    {
        public String Name { get; set; } = String.Empty; // The name of the product, cannot be null or empty.
        public Guid ManufacturerId { get; set; } = Guid.Empty; // The unique identifier of the manufacturer, cannot be null or empty.
    }
}

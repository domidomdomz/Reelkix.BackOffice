namespace Reelkix.BackOffice.Application.Products.Commands.UpdateDraftProduct
{
    public class UpdateDraftProductCommand
    {
        public Guid ProductId { get; set; } = Guid.Empty; // The unique identifier of the product to be updated.
        public string Name { get; set; } = string.Empty; // The name of the product, cannot be null or empty.
        public string Description { get; set; } = string.Empty; // The description of the product, cannot be null or empty.
        public Guid ManufacturerId { get; set; } = Guid.Empty; // The unique identifier of the manufacturer, cannot be null or empty.
        public decimal CostPrice { get; set; } = 0; // The cost price of the product, defaulting to 0.
        public decimal SellingPrice { get; set; } = 0; // The selling price of the product, defaulting to 0.
    }
}
